using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationTriggerArea : MonoBehaviour
{
    /*
    public int id;

    private Collider tempCollider;

    [SerializeField] private GameObject completeItem;

    private bool isComplete = false;

    public bool objectOnStation = false;
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Decoration")
        {
            if (other.gameObject.GetComponent<iDecoration>().GetDecoration().MyDecorationType == DecorationType.RawBauble && !other.gameObject.GetComponent<ItemDecoration>().isBeingHeld)
            {
                objectOnStation = true;
                other.gameObject.GetComponent<RawBaubleHandler>().upgrading = true;

                tempCollider = other;

                


                //Call the event from game events 
                //GameEvents.current.StationHolderTriggerEnter(id);
            }
        }
        
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<RawBaubleHandler>() != null)
        {
            if (other.gameObject.GetComponent<RawBaubleHandler>().completed)
            {
                other.gameObject.GetComponent<RawBaubleHandler>().DestroyDecoration();

                isComplete = true;
                CompleteItem(other);
            }
        }

    }

    private void CompleteItem(Collider bauble)
    {
        if (isComplete)
        {
            GameObject tempCompleteItem = (GameObject)Instantiate(completeItem, transform.position, transform.rotation);
            isComplete = false;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        
        if(other.gameObject.tag == "Decoration")
        {
            if (other.gameObject.GetComponent<iDecoration>().GetDecoration().MyDecorationType == DecorationType.RawBauble && other.gameObject.GetComponent<ItemDecoration>().isBeingHeld)
            {
                objectOnStation = false;
                other.gameObject.GetComponent<RawBaubleHandler>().upgrading = false;
                //GameEvents.current.StationHolderTriggerExit(id);
            }
        }
        
        

            
    }
    */
}
