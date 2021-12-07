using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class StationTriggerAreaTest : MonoBehaviour
{
    public int id;

    private Collider tempCollider;
    [SerializeField] private DecorationType decoExpected;
    [SerializeField] private GameObject completeItem;

    private bool isComplete = false;

    public bool objectOnStation = false;
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Decoration")
        {
            if (other.gameObject.GetComponent<iDecoration>().GetDecoration().MyDecorationType == decoExpected && !other.gameObject.GetComponent<ItemDecoration>().isBeingHeld)
            {
                objectOnStation = true;
                other.gameObject.GetComponent<iDecoration>().SetUpgrading(true);

                tempCollider = other;

                


                //Call the event from game events 
                //GameEvents.current.StationHolderTriggerEnter(id);
            }
        }
        
    }

    public void OnTriggerStay(Collider other)
    {
        if(other.tag == "Decoration" && other.gameObject.GetComponent<iDecoration>().GetDecoration().MyDecorationType == decoExpected)
        {
            if(other.gameObject.GetComponent<iDecoration>().GetUpgradeComplete())
            {

                if (other.gameObject.GetComponent<iDecoration>().GetUpgradeMethod() == UpgradeMethod.simpleAddItemsTogether)
                {
                    other.gameObject.GetComponent<iDecoration>().DestroyDecoration();

                    isComplete = true;
                    CompleteItem();
                    Destroy(this.gameObject.transform.parent.gameObject);
                }
                else
                {
                    other.gameObject.GetComponent<iDecoration>().DestroyDecoration();

                    isComplete = true;
                    CompleteItem();
                }
                

                
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
        
        if(other.gameObject.tag == "Decoration")
        {
            if (other.gameObject.GetComponent<iDecoration>().GetDecoration().MyDecorationType == decoExpected && other.gameObject.GetComponent<ItemDecoration>().isBeingHeld)
            {
                objectOnStation = false;
                other.gameObject.GetComponent<iDecoration>().SetUpgrading(false);
                //GameEvents.current.StationHolderTriggerExit(id);
            }
        }
        
        

            
    }
}
