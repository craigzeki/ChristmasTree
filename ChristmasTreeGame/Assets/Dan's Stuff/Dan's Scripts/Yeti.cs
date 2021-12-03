using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yeti : MonoBehaviour
{
    [SerializeField] private GameObject[] players;
    [SerializeField] private GameObject snowballPrefab;

    [SerializeField] private Transform firePoint;

    private float countdown = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (players.Length == 0)
        {
            
        }
        //Update player count every frame until there are four players
        if (players.Length < 4)
        {
            players = GameObject.FindGameObjectsWithTag("Player");
        }

        


        countdown -= Time.deltaTime;
        if(countdown <= 0f)
        {
            AttackPlayer();
            countdown = 1f;
        }


    }

    private void AttackPlayer()
    {
        if(GetTarget() != null)
        {
            Snowball.target = GetTarget().transform;
            Instantiate(snowballPrefab, firePoint.position, Quaternion.identity);
        }
    }

    private GameObject GetTarget()
    {
        int index = Random.Range(0, players.Length);
        GameObject player = players[index];



        return player;
    }
}
