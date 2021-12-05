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

    [SerializeField] private Transform spawnPoint;

    [SerializeField] private float itemSpawnTime = 1f;
    private bool canSpawnItem = true;

    private PlayerMovement playerMovement1;

    [SerializeField] private List<GameObject> objectList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(itemStats.itemPrefab, transform.position, Quaternion.identity);

        //Get the spawnpoint for this specific holder 
        spawnPoint = this.gameObject.transform.GetChild(1);
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
        Instantiate(itemStats.itemPrefab, spawnPoint.position, Quaternion.identity);
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


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
