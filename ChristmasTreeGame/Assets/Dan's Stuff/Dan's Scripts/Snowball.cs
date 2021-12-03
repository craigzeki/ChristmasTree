using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Snowball : MonoBehaviour
{
    public static Transform target;
    private Rigidbody rb;

    [SerializeField] private float speed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 5f);
    }

    private void FixedUpdate()
    {
        //Direction isn't being calculated correctly (FIX)
        Vector3 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Destroy(this.gameObject);
    }
}
