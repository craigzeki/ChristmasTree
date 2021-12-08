using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RibbonHandler : MonoBehaviour, iDecoration
{
    [SerializeField] private int points = 10;
    [SerializeField] private int multiplier = 1;
    private RibbonDecoration myDeco;

    [SerializeField] private UpgradeMethod upgradeMethod;

    GameObject progressBar;
    [SerializeField] GameObject progressBarPrefab;
    [SerializeField] GameObject barTag;
    GameObject canvas;

    [SerializeField] bool needsProgressBar = false;

    public void DestroyDecoration()
    {
        Destroy(this.gameObject);
    }

    public bool GetPlayerInArea()
    {

        return myDeco.PlayerInArea;
    }

    public void SetPlayerInArea(bool player)
    {
        myDeco.PlayerInArea = player;

    }
    public Collider GetPlayerCollider()
    {
        return myDeco.Collider;
    }

    public void SetPlayerCollider(Collider collider)
    {
        myDeco.Collider = collider;
    }
    public float GetUpgradeProgress()
    {
        return myDeco.Progress;
    }

    public Decoration GetDecoration()
    {
        if (myDeco != null)
        {
            return myDeco;
        }
        else
        {
            return null;
        }
    }

    private void Awake()
    {
        myDeco = new RibbonDecoration(points, multiplier, upgradeMethod);
    }

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Deco Type: " + myDeco.MyDecorationType.ToString() + "  Deco Points: " + myDeco.GetPoints().ToString());
        canvas = GameObject.Find("InGameCanvas");
    }

    // Update is called once per frame
    void Update()
    {
        myDeco.Upgrade(upgradeMethod);
        if (myDeco.Upgrading && progressBar != null && needsProgressBar)
        {
            progressBar.transform.position = Camera.main.WorldToScreenPoint(barTag.transform.position);
            progressBar.GetComponent<ProgressBar>().CurrentStatus((int)(myDeco.Progress));
            if (myDeco.Progress == 100)
            {
                Destroy(progressBar);
            }
        }
    }

    public void SetUpgrading(bool state)
    {
        if (state)
        {
            if (progressBarPrefab != null && progressBar == null && needsProgressBar)
            {
                progressBar = (GameObject)Instantiate(progressBarPrefab, barTag.transform.localPosition, Quaternion.Euler(barTag.transform.localEulerAngles), canvas.transform);
                //progressBar.GetComponent<ProgressBar>().CurrentStatus(Mathf.RoundToInt(myDeco.Progress));
                progressBar.GetComponent<ProgressBar>().CurrentStatus((int)(myDeco.Progress));
            }
        }
        myDeco.Upgrading = true;
    }

    public bool GetUpgradeComplete()
    {
        return myDeco.Completed;
    }
    public UpgradeMethod GetUpgradeMethod()
    {
        return myDeco.MyUpgradeMethod;
    }
}
