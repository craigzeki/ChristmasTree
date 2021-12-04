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

    [SerializeField] private float itemSpawnTime = 1f;
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
        itemSpawnTime -= Time.deltaTime;
        if(itemSpawnTime <= 0f)
        {
            itemSpawnTime = 0f;
            canSpawnItem = true;
        }

        
        CheckPlayerInRange();
        
    }

    private void CheckPlayerInRange()
    {

        Collider[] allPlayersInRange = Physics.OverlapSphere(transform.position, radius, LayerMask.GetMask("Player"));

        if(allPlayersInRange.Length == 0)
        {
            DestroyObjects();
        }

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
        
        if(!holdingObject && canSpawnItem)
        {
            SpawnItem();
        }
        

    }

    private void SpawnItem()
    {
        Instantiate(itemStats.itemPrefab, transform.position, Quaternion.identity);
        canSpawnItem = false;
        itemSpawnTime = 1f;
    }

    
    private void DestroyObjects()
    {
        Collider[] objectsInRange = Physics.OverlapSphere(transform.position, radius, LayerMask.GetMask("Objects"));

        foreach(Collider objects in objectsInRange)
        {
            Destroy(objects.gameObject, 3f);
        }
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
