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
    // Start is called before the first frame update
    void Start()
    {
        manager = GetComponent<PlayerInputManager>();
        index = 0;

        manager.playerPrefab = characters[index];
    }

    public void ChangeAvatarOnJoin(PlayerInput input)
    {
        //Pick a random character from the character array
        //index = Random.Range(0, characters.Length);

        /*
        bool changeCharacter = true;
        if (changeCharacter)
        {
            index++;
            changeCharacter = false;
        }
        if(index != characters.Length)
        {
            Debug.Log(index.ToString());
            
        }
        */
        index = Random.Range(0, characters.Length);
        manager.playerPrefab = characters[index];
    }
    
}
