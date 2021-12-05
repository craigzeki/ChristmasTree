using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCentre : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        
        if (!other.gameObject.GetComponent<ItemDecoration>().isBeingHeld)
        {
            Debug.Log("Placed object");
            other.gameObject.transform.position = transform.GetChild(0).position;
            other.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
