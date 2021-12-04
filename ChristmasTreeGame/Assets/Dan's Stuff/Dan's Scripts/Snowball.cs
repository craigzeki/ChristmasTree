using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Snowball : MonoBehaviour
{
    public static Transform target;
    private CharacterController controller;
    private Rigidbody rb;

    [SerializeField] private float speed = 1f;
    private float rotationForce = 30f;

    private bool snowballFired = false;
    private Vector3 direction = Vector3.zero;

    private bool playerCanMove = true;

    [SerializeField] private float moveTimer = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        //controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        //target = GameObject.FindGameObjectWithTag("Decoration").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!playerCanMove)
        {
            moveTimer -= Time.deltaTime;
            if(moveTimer <= 0f)
            {
                playerCanMove = true;
                moveTimer = 0.5f;
            }
        }

        //controller.Move(direction * speed * Time.deltaTime);

        Destroy(gameObject, 5f);
    }

    private void FixedUpdate()
    {
        if (!snowballFired)
        {
            //Get the postion of the player once so it doesn't track to them
            direction = target.position - transform.position;
            snowballFired = true;
        }
        

        rb.velocity = direction * speed;
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit target");
        if(collision.gameObject.tag == "Player")
        {
            //Freeze the players movement
            PlayerMovement movement = collision.gameObject.GetComponent<PlayerMovement>();
            movement.canMove = false;
            playerCanMove = false;
            Destroy(gameObject);
        }
    }
}
