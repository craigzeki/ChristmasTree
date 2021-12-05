using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationTriggerArea : MonoBehaviour
{
    public int id;

    private Collider tempCollider;


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<ItemDecoration>().name == "Bauble" && !other.gameObject.GetComponent<ItemDecoration>().isBeingHeld)
        {


            tempCollider = other;



            //Call the event from game events 
            GameEvents.current.StationHolderTriggerEnter(id);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        
        
        if (other.gameObject.GetComponent<ItemDecoration>().name == "Bauble" && other.gameObject.GetComponent<ItemDecoration>().isBeingHeld && other == tempCollider)
        {

            GameEvents.current.StationHolderTriggerExit(id);
        }
        

            
    }
}
