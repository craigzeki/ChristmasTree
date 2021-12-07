using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotHandler : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Decoration")
        {
            if(other.gameObject.GetComponent<iDecoration>().GetDecoration().MyDecorationType == DecorationType.Gold &&
                !other.gameObject.GetComponent<ItemDecoration>().isBeingHeld)
            {
                Instantiate(objectToSpawn, this.transform.parent.transform.position, this.transform.parent.transform.rotation);
                Destroy(other.gameObject);
                Destroy(this.transform.parent.gameObject);
            }
        }
    }
    
}
