using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotOMoltenGoldHandler : MonoBehaviour, iDecoration
{
    [SerializeField] private int points = 10;
    [SerializeField] private int multiplier = 1;
    private PotOMoltenGoldDecoration myDeco;

    [SerializeField] private UpgradeMethod upgradeMethod;

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

    public bool GetUpgradeComplete()
    {
        throw new System.NotImplementedException();
    }

    public void SetUpgrading(bool state)
    {
        throw new System.NotImplementedException();
    }

    private void Awake()
    {
        myDeco = new PotOMoltenGoldDecoration(points, multiplier, upgradeMethod);
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
    public UpgradeMethod GetUpgradeMethod()
    {
        return myDeco.MyUpgradeMethod;
    }
}
