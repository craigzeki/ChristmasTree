using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DecorationPlacedState
{
    NotPlaced = 0,
    Placed,
    //New states here,
    NumOfStates
}

public class TreeHandler : MonoBehaviour
{
    [SerializeField] private List<GameObject> starTags = new List<GameObject>();
    [SerializeField] private List<GameObject> baubleTags = new List<GameObject>();
    [SerializeField] private List<GameObject> bowTags = new List<GameObject>();
    [SerializeField] private List<GameObject> giftTags = new List<GameObject>();
    //[SerializeField] List<GameObject> presentTags = new List<GameObject>();


    private List<DecorationPlacedState> starPlaced = new List<DecorationPlacedState>();
    private List<DecorationPlacedState> baublesPlaced = new List<DecorationPlacedState>();
    private List<DecorationPlacedState> bowsPlaced = new List<DecorationPlacedState>();
    private List<DecorationPlacedState> giftsPlaced = new List<DecorationPlacedState>();

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject tag in starTags)
        {
            starPlaced.Add(DecorationPlacedState.NotPlaced);
        }
        foreach (GameObject tag in baubleTags)
        {
            baublesPlaced.Add(DecorationPlacedState.NotPlaced);
        }
        foreach (GameObject tag in bowTags)
        {
            bowsPlaced.Add(DecorationPlacedState.NotPlaced);
        }
        foreach (GameObject tag in giftTags)
        {
            giftsPlaced.Add(DecorationPlacedState.NotPlaced);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaceDecoration(GameObject decorationToPlace)
    {
        bool result = false;
        Decoration myDeco = decorationToPlace.GetComponent<iDecoration>().GetDecoration();
        decorationToPlace.transform.SetParent(this.gameObject.transform);
        switch (myDeco.MyDecorationType)
        {
            case DecorationType.Star:
                if (starTags.Count != 0)
                {
                    result = FindEmptyTagAndPlace(decorationToPlace.transform, starTags, starPlaced);
                }
                else
                {
                    Debug.LogError("Class TreeHandler : PlaceDecoration starTag is not valid (starTag = null)");
                }

                break;
            case DecorationType.Bow:
                if (bowTags.Count != 0)
                {
                    result = FindEmptyTagAndPlace(decorationToPlace.transform, bowTags, bowsPlaced);
                }
                else
                {
                    Debug.LogError("Class TreeHandler : PlaceDecoration starTag is not valid (starTag = null)");
                }
                break;
            case DecorationType.Bauble:
                if (baubleTags.Count != 0)
                {
                    result = FindEmptyTagAndPlace(decorationToPlace.transform, baubleTags, baublesPlaced);
                }
                else
                {
                    Debug.LogError("Class TreeHandler : PlaceDecoration starTag is not valid (starTag = null)");
                }
                break;
            case DecorationType.Gift:
                if (giftTags.Count != 0)
                {
                    result = FindEmptyTagAndPlace(decorationToPlace.transform, giftTags, giftsPlaced);
                }
                else
                {
                    Debug.LogError("Class TreeHandler : PlaceDecoration starTag is not valid (starTag = null)");
                }
                break;
            case DecorationType.NumOfDecorations:
            //removed break to allow flow through as both invalid
            default:
                Debug.LogError("Class TreeHandler : PlaceDecoration MyDecorationType is not valid (MyDecorationType = )" + myDeco.MyDecorationType.ToString());
                break;
        }

        if (result == false)
        {
            decorationToPlace.GetComponent<iDecoration>().DestroyDecoration();
        }
    }

    private bool FindEmptyTagAndPlace(Transform decorationTransform, List<GameObject> decoTags, List<DecorationPlacedState> decorationPlacedStates)
    {
        int i = 0;
        bool result = false;

        for (i = 0; i < decorationPlacedStates.Count; i++)
        {
            if(decorationPlacedStates[i] == DecorationPlacedState.NotPlaced)
            {
                decorationTransform.localPosition = decoTags[i].transform.localPosition;
                decorationTransform.localScale = decoTags[i].transform.localScale;
                decorationPlacedStates[i] = DecorationPlacedState.Placed;
                result = true;
                break;
            }
        }
        return result; ;
    }
}
