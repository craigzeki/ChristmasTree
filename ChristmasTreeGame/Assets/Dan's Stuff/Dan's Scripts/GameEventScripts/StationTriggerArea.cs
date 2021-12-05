using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationTriggerArea : MonoBehaviour
{
    public int id;
    public void OnTriggerEnter(Collider other)
    {
       
        if(other.gameObject.GetComponent<ItemDecoration>().name == "Bauble" && !other.gameObject.GetComponent<ItemDecoration>().isBeingHeld)
        {
            other.gameObject.transform.position = transform.GetChild(0).position;
            other.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;

            //Call the event from game events 
            GameEvents.current.StationHolderTriggerEnter(id);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<ItemDecoration>().name == "Bauble" && other.gameObject.GetComponent<ItemDecoration>().isBeingHeld)
        {

            GameEvents.current.StationHolderTriggerExit(id);
        }


            
    }
}
