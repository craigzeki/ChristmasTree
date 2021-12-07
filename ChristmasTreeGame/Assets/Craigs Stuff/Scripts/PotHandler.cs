using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotHandler : MonoBehaviour, iMobileStation, iDecoration
{
    public enum PotState
    {
        Empty = 0,
        PotOGold,
        PotOMoltenGold,
        //New states here
        NumOfStates
    }

    [SerializeField] List<GameObject> visualForState = new List<GameObject>();

    public int id;

    private Collider tempCollider;
    [SerializeField] private DecorationType decoExpected;

    [SerializeField] private PotState potState = PotState.Empty;

    private bool isComplete = false;

    public bool objectOnStation = false;

    private Decoration myDeco = new Decoration(DecorationType.Pot, 0, UpgradeMethod.NoMethod);

    public void Awake()
    {
        UpdatePot();
    }

    private void UpdatePot()
    {
        switch (potState)
        {
            case PotState.Empty:
                myDeco.MyDecorationType = DecorationType.Pot;
                myDeco.MyUpgradeMethod = UpgradeMethod.NoMethod;
                visualForState[(int)PotState.Empty].SetActive(true);
                visualForState[(int)PotState.PotOGold].SetActive(false);
                visualForState[(int)PotState.PotOMoltenGold].SetActive(false);
                break;
            case PotState.PotOGold:
                myDeco.MyDecorationType = DecorationType.PotOGold;
                myDeco.MyUpgradeMethod = UpgradeMethod.TimeBased;
                visualForState[(int)PotState.Empty].SetActive(true);
                visualForState[(int)PotState.PotOGold].SetActive(true);
                visualForState[(int)PotState.PotOMoltenGold].SetActive(false);
                break;
            case PotState.PotOMoltenGold:
                myDeco.MyDecorationType = DecorationType.PotOMoltenGold;
                myDeco.MyUpgradeMethod = UpgradeMethod.RemoveFrom;
                visualForState[(int)PotState.Empty].SetActive(true);
                visualForState[(int)PotState.PotOGold].SetActive(false);
                visualForState[(int)PotState.PotOMoltenGold].SetActive(true);
                break;
            case PotState.NumOfStates:
                break;
            default:
                break;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Decoration")
        {
            if (other.gameObject.GetComponent<iDecoration>().GetDecoration().MyDecorationType == decoExpected && !other.gameObject.GetComponent<ItemDecoration>().isBeingHeld)
            {
                objectOnStation = true;
                other.gameObject.GetComponent<iDecoration>().SetUpgrading(true);

                tempCollider = other;

            }
        }

    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Decoration" && other.gameObject.GetComponent<iDecoration>().GetDecoration().MyDecorationType == decoExpected)
        {
            if (other.gameObject.GetComponent<iDecoration>().GetUpgradeComplete())
            {

                if (other.gameObject.GetComponent<iDecoration>().GetUpgradeMethod() == UpgradeMethod.AddTogether)
                {
                    other.gameObject.GetComponent<iDecoration>().DestroyDecoration();

                    isComplete = true;

                    UpgradeComplete();
                }
                else
                {
                    other.gameObject.GetComponent<iDecoration>().DestroyDecoration();

                    isComplete = true;
                    UpgradeComplete();
                }



            }
        }
    }



   

    public void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Decoration")
        {
            if (other.gameObject.GetComponent<iDecoration>().GetDecoration().MyDecorationType == decoExpected && other.gameObject.GetComponent<ItemDecoration>().isBeingHeld)
            {
                objectOnStation = false;
                other.gameObject.GetComponent<iDecoration>().SetUpgrading(false);
                //GameEvents.current.StationHolderTriggerExit(id);
            }
        }




    }

    public void Update()
    {
        myDeco.Upgrade(myDeco.MyUpgradeMethod);
    }

    public void UpgradeComplete()
    {
        if (potState++ == PotState.NumOfStates)
        {
            potState = PotState.Empty;
        }
        UpdatePot();
    }

    public Decoration GetDecoration()
    {
        return myDeco;
    }

    public void DestroyDecoration()
    {
        
    }

    public void SetUpgrading(bool state)
    {
        myDeco.Upgrading = true;
    }

    public bool GetUpgradeComplete()
    {
        if(myDeco.Completed)
        {
            UpgradeComplete();
        }
        return myDeco.Completed;
    }

    public UpgradeMethod GetUpgradeMethod()
    {
        return myDeco.MyUpgradeMethod;
    }
}
