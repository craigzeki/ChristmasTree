using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloudHandler : MonoBehaviour
{
    [SerializeField] GameObject[] decorations2D = new GameObject[(int)DecorationType.NumOfDecorations];
    [SerializeField] Sprite[] decorationsComplete2D = new Sprite[(int)DecorationType.NumOfDecorations];
    [SerializeField] private GameObject cloudPrefab;
    private GameObject inPlayCanvas;
    private GameObject myCloud;
    private List<GameObject> myDecorations = new List<GameObject>();
    private List<GameObject> myCompleteIcons = new List<GameObject>();

    private void Awake()
    {
        inPlayCanvas = GameObject.Find("InGameCanvas");
        if(inPlayCanvas != null)
        {
            myCloud = Instantiate(cloudPrefab, inPlayCanvas.transform);
        }
        if (myCloud != null)
        {
            myCloud.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(myCloud != null)
        {
            myCloud.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        }
        
    }

    public void AddDecorations(List<DecorationType> decorations)
    {
        if (myCloud == null) return; //don't process if cloud does not exist

        //add each decoration in order
        for (int i = 0; i < decorations.Count; i++)
        {
            myDecorations.Add(Instantiate(decorations2D[(int)decorations[i]], myCloud.transform));
            myCompleteIcons.Add(myDecorations[i].transform.Find("CompleteIcon").gameObject);
            myCompleteIcons[i].SetActive(false);
        }
    }

    public void markDecorationAsDone(int decorationIndex, DecorationType decorationType)
    {
        if(decorationIndex >= myDecorations.Count)
        {
            Debug.LogError("Class CloudHandler : markDecorationAsDone - decorationIndex >= myDecorations.count");
            return;
        }
        else
        {
            myDecorations[decorationIndex].GetComponent<Image>().sprite = decorationsComplete2D[(int)decorationType];
            myCompleteIcons[decorationIndex].SetActive(true);
        }
        
    }
}
