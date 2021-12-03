using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private float currentTime = 0f;

    protected Transform point;

    [Range(0, 10f)]
    [SerializeField] private float range = 1f;
    public bool holdingObject = false;
    // Start is called before the first frame update
    void Start()
    {
        holdingObject = false;
        //point = GameObject.FindGameObjectWithTag("Point").transform;
        point = this.gameObject.transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= 0.2f && PlayerMovement.grabedObject)
        {
            currentTime = 0f;
            GrabObject();
        }


    }

    public virtual void GrabObject()
    {
        LayerMask objectMask = LayerMask.GetMask("Objects");

        Collider[] objectsHit = Physics.OverlapSphere(point.position, range, objectMask);

        foreach (Collider objects in objectsHit)
        {
            Debug.Log("Grab working");
            if (!holdingObject)
            {
                objects.gameObject.GetComponent<Rigidbody>().useGravity = false;
                objects.transform.parent = gameObject.transform;
                objects.transform.position = point.transform.position;
                holdingObject = true;
            }
            else if (holdingObject)
            {
                objects.gameObject.GetComponent<Rigidbody>().useGravity = true;
                objects.transform.parent = null;
                holdingObject = false;
            }
        }

    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(point.position, range);
    }
}
