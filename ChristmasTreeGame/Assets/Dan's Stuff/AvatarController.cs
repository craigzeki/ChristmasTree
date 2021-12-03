using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AvatarController : MonoBehaviour
{
    private PlayerControls inputActions;

    private void Awake()
    {
        inputActions = new PlayerControls();
        inputActions.Player.Enable();
    }

    private void OnEnable()
    {
        inputActions.Player.Grab.performed += OnGrab;
    }

    private void OnDisable()
    {
        inputActions.Player.Grab.performed -= OnGrab;
    }

    private void Update()
    {
        Vector2 inputVector = inputActions.Player.Movement.ReadValue<Vector2>();
    }

    public void OnGrab(InputAction.CallbackContext context)
    {

    }
}
