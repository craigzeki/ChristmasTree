using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMove : MonoBehaviour
{
    //private CharacterController controller;
    private Rigidbody rb;
    public Transform target;
    public float force = 3f;
    // Start is called before the first frame update
    void Start()
    {
        //controller = GetComponent<CharacterController>();
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        

        

        //controller.Move(direction * force * Time.deltaTime);
    }
    private void FixedUpdate()
    {
        Vector3 direction = target.position - transform.position;
        
        rb.AddForce(direction * force, ForceMode.Impulse);
    }
}
