using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotOGoldHandler : MonoBehaviour, iDecoration
{
    [SerializeField] private int points = 10;
    [SerializeField] private int multiplier = 1;
    private PotOGoldDecoration myDeco;

    [SerializeField] private UpgradeMethod upgradeMethod = UpgradeMethod.noMethod;


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
        myDeco = new PotOGoldDecoration(points, multiplier, upgradeMethod);
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
    public UpgradeMethod GetUpgradeMethod()
    {
        return myDeco.MyUpgradeMethod;
    }
}
