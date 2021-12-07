using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoltenStarHandler : MonoBehaviour, iDecoration
{
    [SerializeField] private int points = 10;
    [SerializeField] private int multiplier = 1;
    private MoltenStarDecoration myDeco;

    [SerializeField] private UpgradeMethod upgradeMethod = UpgradeMethod.NoMethod;

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

    public bool GetUpgradeComplete()
    {
        return myDeco.Completed;
    }

    public UpgradeMethod GetUpgradeMethod()
    {
        return myDeco.MyUpgradeMethod;
    }

    public void SetUpgrading(bool state)
    {
        myDeco.Upgrading = state;
    }

    private void Awake()
    {
        myDeco = new MoltenStarDecoration(points, multiplier, upgradeMethod);
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
}
