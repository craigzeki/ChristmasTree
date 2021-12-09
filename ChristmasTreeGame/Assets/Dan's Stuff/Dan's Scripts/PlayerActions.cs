using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private float currentTime = 0f;

    protected Transform point;

    [Range(0, 10f)]
    [SerializeField] private float range = 1f;

    private PlayerMovement playerMovement;

    [SerializeField] private List<GameObject> objectsInHand = new List<GameObject>();

    [SerializeField] protected bool holdingObject = false;
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = this.gameObject.GetComponent<PlayerMovement>();
        //point = GameObject.FindGameObjectWithTag("Point").transform;
        point = this.gameObject.transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {

        currentTime += Time.deltaTime;
        if (currentTime >= 0.2f && playerMovement.grabedObject)
        {
            currentTime = 0f;
            GrabObject();
            
        }

        if(objectsInHand.Count > 0)
        {
            holdingObject = true;
            foreach(GameObject objectInHand in objectsInHand)
            {
                if(objectInHand != null)
                {
                    objectInHand.transform.position = point.transform.position;
                }
                
            }    
        }
        else
        {
            holdingObject = false;
        }


    }

    public virtual void GrabObject()
    {
        LayerMask objectMask = LayerMask.GetMask("Objects");
        Debug.Log("Player grabbing object");

        Collider[] objectsHit = Physics.OverlapSphere(point.position, range, objectMask);

        foreach (Collider objects in objectsHit)
        {
            
            ItemDecoration item = objects.gameObject.GetComponent<ItemDecoration>();

            Transform currentObjectScale = objects.gameObject.transform;

            if (this.transform.childCount < 3 && !item.isBeingHeld)
            {

                objects.gameObject.GetComponent<Rigidbody>().isKinematic = true;

                objects.transform.localScale = currentObjectScale.localScale;
                //objects.transform.localRotation = currentObjectScale.rotation;

                //objects.transform.localScale = new Vector3(1, 1, 1);
                objects.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

                objects.transform.parent = gameObject.transform;
                objects.transform.position = point.transform.position;
                item.isBeingHeld = true;
                objectsInHand.Add(objects.gameObject);


            }
            else if (this.transform.childCount >= 3)
            {

                objects.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                objects.transform.parent = null;
                item.isBeingHeld = false;

                objectsInHand.Remove(objects.gameObject);


            }
        }

    }



    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(point.position, range);
    }
}
