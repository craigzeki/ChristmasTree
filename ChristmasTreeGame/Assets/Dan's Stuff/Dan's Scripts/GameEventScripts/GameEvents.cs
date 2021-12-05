using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    public event Action<int> onItemHolderTriggerEnter;

    public void ItemHolderTrigerEnter(int id)
    {
        if(onItemHolderTriggerEnter != null)
        {
            onItemHolderTriggerEnter(id);
        }
    }
    public event Action<int> onItemHolderTriggerExit;

    public void ItemTriggerExit(int id)
    {
        if(onItemHolderTriggerExit != null)
        {
            onItemHolderTriggerExit(id);
        }
    }

    public event Action<int> onStationHolderTriggerEnter;

    public void StationHolderTriggerEnter(int id)
    {
        if(onStationHolderTriggerEnter != null)
        {
            onStationHolderTriggerEnter(id);
        }
    }

    public event Action<int> onStationHolderTriggerExit;

    public void StationHolderTriggerExit(int id)
    {
        if(onStationHolderTriggerExit != null)
        {
            onStationHolderTriggerExit(id);
        }
    }

}
