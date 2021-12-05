using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    public int id;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Player in range");
            GameEvents.current.ItemHolderTrigerEnter(id);
        }
        

    }

    private void OnTriggerExit(Collider other)
    {
        GameEvents.current.ItemTriggerExit(id);
    }
}
