using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RawBaubleHandler : MonoBehaviour, iDecoration
{
    [SerializeField] private int points = 10;
    [SerializeField] private int multiplier = 1;
    private RawBaubleDecoration myDeco;

    [SerializeField] private UpgradeMethod upgradeMethod;



    public void DestroyDecoration()
    {
        Destroy(this.gameObject);
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
        myDeco = new RawBaubleDecoration(points, multiplier);
    }

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Deco Type: " + myDeco.MyDecorationType.ToString() + "  Deco Points: " + myDeco.GetPoints().ToString());

    }

    // Update is called once per frame
    void Update()
    {
        myDeco.Upgrade(upgradeMethod);
        
    }

    public void SetUpgrading(bool state)
    {

        myDeco.Upgrading = true;
    }

    public bool GetUpgradeComplete()
    {
        return myDeco.Completed;
    }
}
