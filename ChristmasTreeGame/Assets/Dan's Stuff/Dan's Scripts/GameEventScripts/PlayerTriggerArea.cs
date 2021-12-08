using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerArea : MonoBehaviour
{
    public int id;
    iDecoration decoration;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            iDecoration tempDecoration = other.gameObject.GetComponentInChildren<iDecoration>();
          
            if (tempDecoration != null)
            {
                decoration = tempDecoration;
                decoration.SetPlayerInArea(true);
                decoration.SetPlayerCollider(other);
            }
            if(decoration != null)
            {
                decoration.SetPlayerInArea(true);
                decoration.SetPlayerCollider(other);
            }

            //TO DO
            //Set decoration back to null when the decoration interaction is finished

            //GameEvents.current.StationHolderTriggerEnter(id);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {


            if (decoration != null)
            {
                decoration.SetPlayerInArea(false);
                decoration.SetPlayerCollider(null);
            }
        }
        
        
    }

    private void Update()
    {

        if (decoration != null)
        {

            Debug.Log("Decoration here" + decoration.GetPlayerInArea().ToString());
        }
        
    }
}
