using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerArea : MonoBehaviour
{
    public int id;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            bool isInteracting = other.gameObject.GetComponent<PlayerMovement>().interactPressed;
            if (isInteracting)
            {
                Debug.Log("Player interacting with station");
            }
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
