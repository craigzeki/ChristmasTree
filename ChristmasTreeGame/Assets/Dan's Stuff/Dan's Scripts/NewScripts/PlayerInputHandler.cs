using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    PlayerMovement playerMovement;

    [SerializeField] private List<GameObject> prefabs = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GameObject.Instantiate(prefabs[Random.Range(0, prefabs.Count)], transform.position, transform.rotation).GetComponent<PlayerMovement>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (playerMovement)
        {
            //playerMovement.OnMove(context.ReadValue<Vector2>());
        }
    }

    public void OnGrab(InputAction.CallbackContext context)
    {
        if(playerMovement && context.started)
        {
            playerMovement.OnGrab(context);
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if(playerMovement && context.started)
        {
            playerMovement.OnInteract(context);
        }
    }
}
