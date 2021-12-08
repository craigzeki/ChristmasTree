using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class StationTriggerAreaTest : MonoBehaviour
{
    public int id;

    private Collider tempCollider;
    [SerializeField] private DecorationType decoExpected;
    [SerializeField] private GameObject completeItem;
    [SerializeField] private GameObject interactableStationObject;
    [SerializeField] private GameObject interactableObjectLocationTag;

    private DecorationType backupDecoType;

    private bool isComplete = false;

    public bool objectOnStation = false;
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Decoration")
        {
            if (other.gameObject.GetComponentInChildren<iDecoration>().GetDecoration().MyDecorationType == decoExpected && !other.gameObject.GetComponent<ItemDecoration>().isBeingHeld)
            {
                objectOnStation = true;
                other.gameObject.GetComponentInChildren<iDecoration>().SetUpgrading(true);

                tempCollider = other;

                


                //Call the event from game events 
                //GameEvents.current.StationHolderTriggerEnter(id);
            }
        }
        else if(other.gameObject.tag == "MoveableObject")
        {
            if (other.gameObject.GetComponentInChildren<iDecoration>().GetDecoration().MyDecorationType == decoExpected)
            {
                
                other.gameObject.GetComponentInChildren<iDecoration>().SetUpgrading(true);

                tempCollider = other;




                //Call the event from game events 
                //GameEvents.current.StationHolderTriggerEnter(id);
            }
        }
        
    }

    public void OnTriggerStay(Collider other)
    {
        if(other.tag == "Decoration" && other.gameObject.GetComponentInChildren<iDecoration>().GetDecoration().MyDecorationType == decoExpected)
        {
            if(other.gameObject.GetComponentInChildren<iDecoration>().GetUpgradeComplete())
            {
                decoExpected = backupDecoType;
                if (other.gameObject.GetComponentInChildren<iDecoration>().GetUpgradeMethod() == UpgradeMethod.AddTogether)
                {
                    other.gameObject.GetComponentInChildren<iDecoration>().DestroyDecoration();

                    isComplete = true;
                    CompleteItem();
                    Destroy(this.gameObject.transform.parent.gameObject);
                }
                else
                {
                    other.gameObject.GetComponentInChildren<iDecoration>().DestroyDecoration();

                    isComplete = true;
                    CompleteItem();
                }
                

                
            }
            else
            {
                
                other.gameObject.GetComponentInChildren<iDecoration>().SetUpgrading(true);
            }
            
        }
        else if (other.tag == "MoveableObject" && other.gameObject.GetComponentInChildren<iDecoration>().GetDecoration().MyDecorationType == decoExpected)
        {
            if (other.gameObject.GetComponentInChildren<iDecoration>().GetUpgradeComplete())
            {

                isComplete = true;
                CompleteMobileStation(other.gameObject);

            }
            else
            {
                other.gameObject.GetComponentInChildren<iDecoration>().SetUpgrading(true);
            }
        }

        /*
        if (other.gameObject.GetComponent<RawBaubleHandler>() != null)
        {
            if (other.gameObject.GetComponent<RawBaubleHandler>().completed)
            {
                other.gameObject.GetComponent<RawBaubleHandler>().DestroyDecoration();

                isComplete = true;
                CompleteItem(other);
            }
        }
        */
    }

    private void CompleteMobileStation(GameObject mobileStation)
    {
        if(isComplete)
        {
            if (mobileStation.GetComponentInChildren<iDecoration>().GetUpgradeMethod() == UpgradeMethod.RemoveFrom)
            {
                if(interactableStationObject != null)
                {
                    GameObject temp = Instantiate(interactableStationObject, interactableObjectLocationTag.transform.position, interactableObjectLocationTag.transform.rotation);
                    backupDecoType = decoExpected;
                    decoExpected = temp.GetComponentInChildren<iDecoration>().GetDecoration().MyDecorationType;
                    objectOnStation = true;
                }    
            }
            mobileStation.GetComponentInChildren<iMobileStation>().UpgradeComplete();
            
            isComplete = false;
        }
    }    

    private void CompleteItem()
    {
        if (isComplete)
        {
            GameObject tempCompleteItem = (GameObject)Instantiate(completeItem, this.gameObject.transform.parent.transform.position, this.gameObject.transform.parent.transform.rotation);
            isComplete = false;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        
        if(other.gameObject.tag == "Decoration" || other.gameObject.tag == "MoveableObject")
        {
            if (other.gameObject.GetComponentInChildren<iDecoration>().GetDecoration().MyDecorationType == decoExpected && other.gameObject.GetComponent<ItemDecoration>().isBeingHeld)
            {
                objectOnStation = false;
                other.gameObject.GetComponentInChildren<iDecoration>().SetUpgrading(false);
                //GameEvents.current.StationHolderTriggerExit(id);
            }
        }
        
        

            
    }

    private void Start()
    {
        backupDecoType = decoExpected;
    }
}
