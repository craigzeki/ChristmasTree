using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDecoration : MonoBehaviour
{

    //Put this script on all items 

    //This script will need to check the parent of the item to see if a player is currently holding it 

    //If a player is holding this item set a bool variable so others can't pick it up

    public string name;

    public bool isBeingHeld = false;

    [SerializeField] private List<GameObject> collidingObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

        

        if(transform.parent != null && transform.parent.parent != null)
        {
            
            isBeingHeld = true;
        }
        else
        {
            isBeingHeld = false;
        }
    }

}
