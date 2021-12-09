using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawning : MonoBehaviour
{
    private Transform spawnpoint;
    [SerializeField] string spawnpointName;

    int tryAFew = 50;

    void Start()
    {
        spawnpoint = GameObject.Find(spawnpointName).transform;
        this.gameObject.transform.position = spawnpoint.position;
    }

    void Update()
    {
        if(tryAFew != 0)
        {
            tryAFew--;
            spawnpoint = GameObject.Find(spawnpointName).transform;
            this.gameObject.transform.position = spawnpoint.position;
            
        }
    }
}
