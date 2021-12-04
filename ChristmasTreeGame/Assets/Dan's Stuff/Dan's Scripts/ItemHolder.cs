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
    [SerializeField] private float radius = 1f;

    private float itemSpawnTime = 1f;
    private bool canSpawnItem = true;

    private PlayerMovement playerMovement1;

    [SerializeField] private List<GameObject> objectList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(itemStats.itemPrefab, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        //itemSpawnTime -= Time.deltaTime;
        if(itemSpawnTime <= 0f)
        {
            itemSpawnTime = 0f;
            canSpawnItem = true;
        }

        /*
        if (CheckPlayerInRange() && PlayerMovement.grabedObject && objectsInHand.Count < 1)
        {
            PickUpItem();
            itemSpawnTime = 1f;
        }
        */
        CheckPlayerInRange();
        
    }

    private void CheckPlayerInRange()
    {

        Collider[] allPlayersInRange = Physics.OverlapSphere(transform.position, radius, LayerMask.GetMask("Player"));

        foreach (Collider player in allPlayersInRange)
        {
            
            PlayerMovement tempPlayerMovement = player.gameObject.GetComponent<PlayerMovement>();

            if (tempPlayerMovement.grabedObject)
            {
                Debug.Log("Player pressed grab");
                CanSpawnItem();
            }
            
        }

    }

    private void CanSpawnItem()
    {
        Collider[] objectsInRange = Physics.OverlapSphere(transform.position, radius, LayerMask.GetMask("Objects"));

        
    }

    private void SpawnItem()
    {
        Instantiate(itemStats.itemPrefab, transform.position, Quaternion.identity);
    }

    private void PickUpItem()
    {
        Debug.Log(itemStats.itemPrefab.ToString());
        if (hasSpawnedItem())
        {
            Debug.Log("Spawning item");
            Instantiate(itemStats.itemPrefab, transform.position, Quaternion.identity);
            canSpawnItem = false;
        }
        
    }

    

    private bool hasSpawnedItem()
    {
        Collider[] objectsInRange = Physics.OverlapSphere(transform.position, radius, LayerMask.GetMask("Objects"));

        if(objectsInRange.Length <= 1)
        {
            return true;
        }
        return false;
    }

    /*
    public override void GrabObject()
    {
        if(PlayerMovement.grabedObject && !holdingObject)
        {
            Instantiate(itemStats.itemPrefab, point.position, Quaternion.identity);
        }
        base.GrabObject();
    }
    */
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
