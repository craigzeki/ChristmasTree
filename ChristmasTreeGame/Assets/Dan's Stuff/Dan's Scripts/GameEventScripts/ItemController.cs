using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public int id;
    [SerializeField] private GameObject itemPrefab;
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onItemHolderTriggerEnter += OnSpawnItem;
        GameEvents.current.onItemHolderTriggerExit += OnStopSpawnItem;
    }

    private void OnSpawnItem(int id)
    {
        if(id == this.id)
        {
            Debug.Log("Spawning Item");
            Instantiate(itemPrefab, transform.position, Quaternion.identity);
        }
        
    }

    private void OnStopSpawnItem(int id)
    {
        if(id == this.id)
        {
            Debug.Log("stop spawn item");
        }
        
    }

    private void OnDestroy()
    {
        GameEvents.current.onItemHolderTriggerEnter -= OnSpawnItem;
        GameEvents.current.onItemHolderTriggerExit -= OnStopSpawnItem;
    }
}
