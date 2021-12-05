using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public enum OrderManagerState : int
{
    NotStarted = 0,
    Running,
    Paused,
    Stopped,
    //New states here
    NumOfStates
}
public class OrderManager : MonoBehaviour
{
    [SerializeField] private GameObject sleighPrefab;
    [SerializeField] private GameObject[] christmasTreePrefabs = new GameObject[(int)ChristmasTreeSize.NumOfSizes];
    [SerializeField] private float[] christmasTreeSpeeds = new float[(int)ChristmasTreeSize.NumOfSizes];
    [SerializeField] private Vector3 christmasTreeOffset = new Vector3(-1.75f, 0.5f, 0.0f);
    [SerializeField] private Vector3 orderStartPositon = Vector3.zero;
    [SerializeField] private Vector3 orderStartRotation = Vector3.zero;
    [SerializeField] private float orderOOBXPosition = 28.5f;
    [SerializeField] private int distancePercentageBuffer = 2;
    [SerializeField] private int minimumDistancePercentage = 30;
    [SerializeField] private Text scoreText;

    private List<ChristmasTreeOrder> christmasTreesOrdered;
    private List<GameObject> ordersOnScreen = new List<GameObject>();
    private OrderManagerState currentState = OrderManagerState.NotStarted;
    private OrderManagerState newState = OrderManagerState.NotStarted;
    private int currentOrder = 0;
    private int orderPoints = 0;
    private List<GameObject> ordersToDestroy = new List<GameObject>();

    public OrderManagerState CurrentState { get => currentState; }
    public OrderManagerState NewState
    {
        set
        {
            newState = value;
            HandleOrderManagerTransitions();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentState = OrderManagerState.NotStarted;
        newState = OrderManagerState.NotStarted;

        List<DecorationType> justStar = new List<DecorationType> { DecorationType.Star };
        List<DecorationType> starBauble = new List<DecorationType> { DecorationType.Star, DecorationType.Bauble };
        List<DecorationType> BowBaubleBaubleStar = new List<DecorationType> { DecorationType.Bow, DecorationType.Bauble, DecorationType.Bauble, DecorationType.Star };
        
        ChristmasTreeOrder order1 = new ChristmasTreeOrder(justStar, christmasTreeSpeeds[(int)ChristmasTreeSize.Small], ChristmasTreeSize.Small);
        ChristmasTreeOrder order2 = new ChristmasTreeOrder(starBauble, christmasTreeSpeeds[(int)ChristmasTreeSize.Large], ChristmasTreeSize.Large);
        ChristmasTreeOrder order3 = new ChristmasTreeOrder(justStar, christmasTreeSpeeds[(int)ChristmasTreeSize.Small], ChristmasTreeSize.Small);
        ChristmasTreeOrder order4 = new ChristmasTreeOrder(BowBaubleBaubleStar, christmasTreeSpeeds[(int)ChristmasTreeSize.Large], ChristmasTreeSize.Large);

        christmasTreesOrdered = new List<ChristmasTreeOrder> { order1, order2, order3, order4 };
    }

    private void OnStart()
    {
        //debug only
        
        StartPressed();
        
    }

    private void OnSpawnOrder()
    {
        // debug only
        if (currentState == OrderManagerState.NotStarted)
        {
            NewState = OrderManagerState.Running;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        
        HandleStateActions();
    }

    private void HandleStateActions()
    {

        switch (currentState)
        {
            case OrderManagerState.NotStarted:
                
                break;
            case OrderManagerState.Running:

                //check if orders on the screen are now OOB and need points collecting and destroying
                ordersToDestroy.Clear();
                //TODO: Need to collect the sleighs destroyed and remove from ordersOnScreen after the loop has exited
                foreach(GameObject sleigh in ordersOnScreen)
                {
                    if (
                        //order has got the end of screen
                        (christmasTreesOrdered[sleigh.GetComponent<OrderHandler>().MyOrderIndex].OrderDistancePercentage == 100)
                      )
                    {
                        if (
                            //order has all the decorations placed
                            (christmasTreesOrdered[sleigh.GetComponent<OrderHandler>().MyOrderIndex].CheckAllDecorationsFulfilled())
                          )
                        {
                            //add points to total
                            orderPoints += christmasTreesOrdered[sleigh.GetComponent<OrderHandler>().MyOrderIndex].Points;

                            //update the gui with the latest score
                            scoreText.text = "Points: " + orderPoints.ToString();
                        }
                        //add to a list so that they can be destroyed after the for loop has completed
                        //otherwise we will be modifying the list and indexing will be messed up
                        ordersToDestroy.Add(sleigh);

                    }
                }

                //go through orders to destroy and remove
                foreach (GameObject sleigh in ordersToDestroy)
                {
                    //destroy the order on screen
                    sleigh.GetComponent<OrderHandler>().DestroyOrder();
                    //remove from screen tracking list
                    ordersOnScreen.Remove(sleigh);
                }

                // instantiate next order
                TryCreateOrder();
                
                break;
            case OrderManagerState.Paused:
                break;
            case OrderManagerState.Stopped:
                break;
            case OrderManagerState.NumOfStates:
            //break; break removed to allow flow to default state as both are invalid
            default:
                Debug.LogError("Class OrderManager : HandleStateActions currentState is not valid (currentState = )" + currentState.ToString());
                break;
        }
    }

    private void HandleOrderManagerTransitions()
    {


        switch (newState)
        {
            case OrderManagerState.NotStarted:
                //not valid transition - should not go back to NotStarted - reset newState
                newState = currentState;
                break;
            case OrderManagerState.Running:
                if(currentState == OrderManagerState.NotStarted)
                {
                    //From NotStarted to Running
                    //clear the list of orders on the screen
                    ordersOnScreen.Clear();
                    //reset currentOrder count
                    currentOrder = 0;

                    currentState = newState;
                }
                if(currentState == OrderManagerState.Paused)
                {
                    //From Paused to Running
                    foreach (GameObject sleigh in ordersOnScreen)
                    {
                        sleigh.GetComponent<OrderHandler>().paused = false;
                    }
                    currentState = newState;
                }
                else
                {
                    //no valid transitions
                    newState = currentState; //reset newState
                }
                break;
            case OrderManagerState.Paused:
                if(currentState == OrderManagerState.Running)
                {
                    //From Running To Paused
                    foreach(GameObject sleigh in ordersOnScreen)
                    {
                        sleigh.GetComponent<OrderHandler>().paused = true;
                    }
                    currentState = newState;
                }
                else
                {
                    //no valid transitions
                    newState = currentState; //reset newState
                }
                break;
            case OrderManagerState.Stopped:
                break;
            case OrderManagerState.NumOfStates:
                //break; break removed to allow flow to default state as both are invalid
            default:
                Debug.LogError("Class OrderManager : HandleOrderManager newState is not valid (newState = )" + newState.ToString());
                break;
        }
    }

    private void TryCreateOrder()
    {
        if(currentOrder < christmasTreesOrdered.Count)
        {
            if (ordersOnScreen.Count == 0)
            {
                // instantiate first order

                CreateOrder();
            }
            else if(currentOrder > 0)
            {
                if(
                    (
                        //order on screen already is faster than the one we want to create
                        christmasTreesOrdered[currentOrder-1].Speed >= christmasTreesOrdered[currentOrder].Speed
                    )
                    &&
                    (
                        //order on screen has already travelled a minimum distance
                        christmasTreesOrdered[currentOrder - 1].OrderDistancePercentage >= minimumDistancePercentage
                    )
                  )
                {
                    CreateOrder();
                }
                else if(
                        //order on screen is slower than the one we want to create
                        //order on screen has travelled far enough such that the new order will not catch up
                        (christmasTreesOrdered[currentOrder - 1].OrderDistancePercentage - distancePercentageBuffer)
                        > ((christmasTreesOrdered[currentOrder - 1].Speed / christmasTreesOrdered[currentOrder].Speed) * 100)
                        )
                {
                    CreateOrder();
                }
            }
        }

    }

    private void CreateOrder()
    {
        GameObject sleigh = Instantiate(sleighPrefab, orderStartPositon, Quaternion.Euler(orderStartRotation));
        // add the correct tree size
        GameObject sleighTree = Instantiate(christmasTreePrefabs[(int)christmasTreesOrdered[currentOrder].ChristmasTreeSize], sleigh.transform);
        sleighTree.transform.position = christmasTreeOffset;
        sleigh.GetComponent<OrderHandler>().MyOrder = christmasTreesOrdered[currentOrder];
        sleigh.GetComponent<OrderHandler>().SetOrderData(currentOrder, orderStartPositon.x, orderOOBXPosition);
        ordersOnScreen.Add(sleigh);
        currentOrder++;
    }

    public void StartPressed()
    {
        switch (currentState)
        {
            case OrderManagerState.NotStarted:
                break;
            case OrderManagerState.Running:
                NewState = OrderManagerState.Paused;
                break;
            case OrderManagerState.Paused:
                NewState = OrderManagerState.Running;
                break;
            case OrderManagerState.Stopped:
                break;
            case OrderManagerState.NumOfStates:
                //break; break removed to allow flow to default state as both are invalid
            default:
                Debug.LogError("Class OrderManager : StartPressed currentState is not valid (currentState = )" + currentState.ToString());
                break;
        }
    }
}
