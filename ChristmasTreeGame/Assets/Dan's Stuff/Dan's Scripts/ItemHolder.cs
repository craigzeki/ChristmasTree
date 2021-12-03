using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : PlayerActions
{
    [System.Serializable]
    public class ItemStats
    {
        public string name;
        public GameObject itemPrefab;
    }

    public ItemStats itemStats;
    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(itemStats.itemPrefab, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void GrabObject()
    {
        if(PlayerMovement.grabedObject && !holdingObject)
        {
            Instantiate(itemStats.itemPrefab, point.position, Quaternion.identity);
        }
        base.GrabObject();
    }
}
