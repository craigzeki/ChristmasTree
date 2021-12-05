using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationController : MonoBehaviour
{
    public int id;
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onStationHolderTriggerEnter += OnInteractItem;
        GameEvents.current.onStationHolderTriggerExit += OnExitItem;
    }

    private void OnInteractItem(int id)
    {
        if(id == this.id)
        {
            
            Debug.Log("Station working");
        }
    }

    private void OnExitItem(int id)
    {
        if(id == this.id)
        {
            Debug.Log("Leaving station");
        }
    }
}
