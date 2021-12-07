using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterSwitcher : MonoBehaviour
{
    int index = 0;
    private PlayerInputManager manager;
    //[SerializeField] private List<GameObject> avatars = new List<GameObject>();
    [SerializeField] private GameObject[] characters;

    private PlayerInput playerInputNew;

    public InputActionAsset inputActions;
    // Start is called before the first frame update
    void Start()
    {
        manager = GetComponent<PlayerInputManager>();
        index = 0;

        manager.playerPrefab = characters[index];
    }

    public void ChangeAvatarOnJoin(PlayerInput input)
    {
        Debug.Log(index);
        /*
        index++;
        if (index >= characters.Length - 1)
        {
            index = 0;
        }
        */

        //index = Random.Range(0, characters.Length);
        if(index <= characters.Length)
        {
            index++;
        }
        
        manager.playerPrefab = characters[index];
        

        /*
        if (manager.playerPrefab.GetComponentInChildren<PlayerInput>().actions == null)
        {
            //playerInputNew = manager.playerPrefab.GetComponentInChildren<PlayerInput>();
            manager.playerPrefab.AddComponent<PlayerInput>().actions = inputActions;
        }
        */

        /*
        if (manager.playerPrefab.GetComponentInChildren<PlayerInput>().actions == null)
        {
            Debug.Log("Player has an action component");
            manager.playerPrefab.GetComponentInChildren<PlayerInput>().actions = inputActions;
        }
        */
    }

    private void Update()
    {
        
        GameObject[] playerList = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject players in playerList)
        {
            if(players.gameObject.GetComponent<PlayerInput>().actions == null)
            {
                players.gameObject.GetComponent<PlayerInput>().actions = inputActions;
                Debug.Log("Adding component");
            }
            
        }
        
        
    }

    private void AddComponentToChild()
    {
        foreach (Transform child in manager.playerPrefab.transform)
        {
            if(child.gameObject.name == "Player")
            {
                Debug.Log("Player here");
            }
        }
    }
    
}
