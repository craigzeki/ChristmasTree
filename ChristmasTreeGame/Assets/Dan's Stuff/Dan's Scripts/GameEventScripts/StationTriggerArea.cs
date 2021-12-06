using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationTriggerArea : MonoBehaviour
{
    public int id;

    private Collider tempCollider;

    public bool objectOnStation = false;
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Decoration")
        {
            if (other.gameObject.GetComponent<iDecoration>().GetDecoration().MyDecorationType == DecorationType.RawBauble && !other.gameObject.GetComponent<ItemDecoration>().isBeingHeld)
            {
                objectOnStation = true;
                other.gameObject.GetComponent<RawBaubleHandler>().upgrading = true; ;
                
                //Call the event from game events 
                //GameEvents.current.StationHolderTriggerEnter(id);
            }
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
}
