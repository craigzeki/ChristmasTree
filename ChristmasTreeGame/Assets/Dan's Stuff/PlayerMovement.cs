using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravity = -9.81f;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    private Vector2 movementInput = Vector2.zero;
    private bool jumped = false;
    


    [Header("Grab variables")]

    private bool grabedObject = false;
    [SerializeField] private Transform point;
    [Range(0, 10f)]
    [SerializeField] private float range = 1f;
    [SerializeField]private bool holdingObject = false;
    private bool holdingTimer = false;
    private bool isButtonDown = false;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        holdingObject = false;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        jumped = context.action.triggered;
    }

    public void OnGrab(InputAction.CallbackContext context)
    {
        grabedObject = context.action.triggered;
        
    }


    // Update is called once per frame
    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if(groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);
        controller.Move(move * Time.deltaTime * playerSpeed);

        if(move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        if(jumped && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }


        if (grabedObject)
        {
            GrabObject();
        }


        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    private void GrabObject()
    {
        LayerMask objectMask = LayerMask.GetMask("Objects");

        Collider[] objectsHit = Physics.OverlapSphere(point.position, range, objectMask);

        foreach(Collider objects in objectsHit)
        {
            Debug.Log("Grab working");
            if (!holdingObject)
            {
                objects.transform.parent = gameObject.transform;
                objects.transform.position = point.transform.position;
                holdingObject = true;
            }
            else if (holdingObject)
            {
                objects.transform.parent = null;
                holdingObject = false;
            }
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(point.position, range);
    }
}
