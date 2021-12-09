using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawning : MonoBehaviour
{
    private Transform spawnpoint;
    [SerializeField] string spawnpointName;

    

    void Start()
    {
        spawnpoint = GameObject.Find(spawnpointName).transform;
        this.gameObject.transform.position = spawnpoint.position;
    }
}
