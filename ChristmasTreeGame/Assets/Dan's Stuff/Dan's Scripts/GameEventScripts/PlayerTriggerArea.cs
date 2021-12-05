using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerArea : MonoBehaviour
{
    public int id;
    private bool interact = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            interact = true;
            //GameEvents.current.StationHolderTriggerEnter(id);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            //GameEvents.current.StationHolderTriggerExit(id);
        }
        
        
    }
}
