using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderHandler : MonoBehaviour
{
    [SerializeField] private GameObject christmasTreeTag;
    private ChristmasTreeOrder myOrder;
    private float myOOBXPosition;
    private float myStartXPosition;
    public bool paused = false;
    private float distanceToTravel = default;
    private int distanceTravelledPercent = 0;
    private CloudHandler myCloud;
    private int myOrderIndex;
    private Bounds myBounds;
    private GameObject myTree;

    public ChristmasTreeOrder MyOrder { get => myOrder;  set => myOrder = value; }
    public int MyOrderIndex { get => myOrderIndex; }

    public void SetOrderData(int orderIndex, float startXPosition, float oobXPosition, GameObject christmasTree)
    {
        myOrderIndex = orderIndex;
        myStartXPosition = Mathf.RoundToInt(startXPosition);
        myOOBXPosition = Mathf.RoundToInt(oobXPosition);
        distanceToTravel = myOOBXPosition - myStartXPosition;
        myTree = Instantiate(christmasTree, this.gameObject.transform);
        myTree.transform.localPosition = christmasTreeTag.transform.localPosition;
        RefreshBounds();
    }

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        myCloud = GetComponentInChildren<CloudHandler>();
        myCloud.AddDecorations(myOrder.DecorationsRequired);

        RefreshBounds();
    }

    public void RefreshBounds()
    {
        myBounds = new Bounds(transform.position, Vector3.zero);
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            myBounds.Encapsulate(r.bounds);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if(myOrder != null && paused == false)
        {
            if (transform.position.x >= myOOBXPosition)
            {
                myOrder.OrderDistancePercentage = 100;
            }
            else
            {
                transform.position += new Vector3(myOrder.Speed, 0, 0) * Time.deltaTime;
                float distanceTravelled = transform.position.x - myStartXPosition;
                distanceTravelledPercent = Mathf.RoundToInt((distanceTravelled / (distanceToTravel + myBounds.size.x)) * 100.0f);
                myOrder.OrderDistancePercentage = distanceTravelledPercent;
                Debug.Log("Distance Travelled %: " + myOrder.OrderDistancePercentage.ToString());
            }
        }
    }

    public void DestroyOrder()
    {
        Destroy(this.gameObject);
    }

    private bool TryPlaceDecoration(Decoration decoration)
    {
        bool result = false;

        for (int i = 0; i < myOrder.DecorationsRequired.Count; i++)
        {
            //check if is needed for the order and hasn't been placed yet
            if (myOrder.DecorationsRequired[i] == decoration.MyDecorationType)
            {
                result = myOrder.CheckAndPlaceDecoration(i, decoration.GetPoints());

                if (result)
                {
                    myCloud.markDecorationAsDone(i, decoration.MyDecorationType);
                    break;
                }
            }
        }

        return result;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Decoration")
        {
            iDecoration theDecoration = other.GetComponent<iDecoration>();
            
            if (theDecoration != null)
            {
                if(TryPlaceDecoration(theDecoration.GetDecoration()))
                {
                    // decoration placed
                    // play placed sound
                    // tell the decoration to destroy itself
                    myTree.GetComponent<TreeHandler>().PlaceDecoration(other.gameObject);
                    //theDecoration.DestroyDecoration();
                    Debug.Log("Order Points: " + myOrder.Points.ToString());
                }
                else
                {
                    // play error sound
                }
            }
            else
            {
                Debug.LogError("Class: OrderHandler : OnTriggerEnter: The object with tag 'Decoration' does not contain a Decoration component");
            }
        }
        
    }
}
