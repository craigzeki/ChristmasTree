using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    private List<ChristmasTreeOrder> christmasTreesOrdered;
    private List<GameObject> ordersOnScreen = new List<GameObject>();
    private OrderManagerState currentState = OrderManagerState.NotStarted;
    private OrderManagerState newState = OrderManagerState.NotStarted;
    private int currentOrder = 0;

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
        ChristmasTreeOrder order1 = new ChristmasTreeOrder(justStar, christmasTreeSpeeds[(int)ChristmasTreeSize.Small], ChristmasTreeSize.Small);
        ChristmasTreeOrder order2 = new ChristmasTreeOrder(starBauble, christmasTreeSpeeds[(int)ChristmasTreeSize.Large], ChristmasTreeSize.Large);

        christmasTreesOrdered = new List<ChristmasTreeOrder> { order2, order1 };
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
                
                if((ordersOnScreen.Count == 0) && (currentOrder < christmasTreesOrdered.Count))
                {
                    // instantiate next order
                    
                    GameObject sleigh = Instantiate(sleighPrefab, orderStartPositon, Quaternion.Euler(orderStartRotation));
                    // add the correct tree size
                    GameObject sleighTree = Instantiate(christmasTreePrefabs[(int)christmasTreesOrdered[currentOrder].ChristmasTreeSize], sleigh.transform);
                    sleighTree.transform.position = christmasTreeOffset;
                    sleigh.GetComponent<OrderHandler>().MyOrder = christmasTreesOrdered[currentOrder];
                    sleigh.GetComponent<OrderHandler>().SetPositions(orderStartPositon.x, orderOOBXPosition);
                    ordersOnScreen.Add(sleigh);
                    currentOrder++;
                }
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
