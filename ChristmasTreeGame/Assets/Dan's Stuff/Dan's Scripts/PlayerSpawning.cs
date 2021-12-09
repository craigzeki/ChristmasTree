using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawning : MonoBehaviour
{
    [SerializeField] private Transform spawnpoint;

    private void Awake()
    {
        this.gameObject.transform.position = spawnpoint.position;
    }
}
