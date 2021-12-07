using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    public int id;
    [SerializeField] private bool canSpawn = true;
    private Collider tempCollider;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Player in range");
            if (canSpawn)
            {
                GameEvents.current.ItemHolderTrigerEnter(id);
            }
            
        }
        

    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Decoration")
        {
            canSpawn = false;
        }
        else
        {
            canSpawn = true;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        GameEvents.current.ItemTriggerExit(id);
    }
}
