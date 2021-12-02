using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderHandler : MonoBehaviour
{
    private ChristmasTreeOrder myOrder;

    public void SetMyOrder(ChristmasTreeOrder treeOrder)
    {
        myOrder = treeOrder;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(myOrder != null)
        {
            transform.position += new Vector3(myOrder.Speed, 0, 0);
        }
        
    }
}
