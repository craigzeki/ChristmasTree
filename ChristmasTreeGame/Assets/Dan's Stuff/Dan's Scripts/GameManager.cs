using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{

    public GameObject[] playerPrefab;
    public int numOfPlayers;

    private List<PlayerMovement> activePlayers;

    public InputActionAsset inputActions;

    // Start is called before the first frame update
    void Start()
    {
        activePlayers = new List<PlayerMovement>();
        for (int i = 0; i < numOfPlayers; i++)
        {
            GameObject tempPlayer = playerPrefab[i];
            GameObject spawnedPlayer = Instantiate(tempPlayer, transform.position, transform.rotation) as GameObject;
            AddPlayerToList(spawnedPlayer.GetComponent<PlayerMovement>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] playerList = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject players in playerList)
        {
            if (players.gameObject.GetComponent<PlayerInput>().actions == null)
            {
                players.gameObject.GetComponent<PlayerInput>().actions = inputActions;
                Debug.Log("Adding component");
            }

        }
    }

    void AddPlayerToList(PlayerMovement newPlayer)
    {
        activePlayers.Add(newPlayer);
    }
}
