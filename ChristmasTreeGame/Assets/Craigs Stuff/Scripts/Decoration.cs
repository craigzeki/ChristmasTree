using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DecorationType : int
{
    Star = 0,
    Bow,
    Bauble,
    Gift,
    RawBauble,
    Gold,
    Pot,
    PotOGold,
    PotOMoltenGold,
    //New decorations here
    NumOfDecorations
}
public enum UpgradeMethod : int { noMethod = 0, timeBased, buttonMash, numOfUpgradeMethods }
public class Decoration
{

    

    private DecorationType myDecorationType;

    //protected so that the children classes can access
    protected int points = 0;

    public DecorationType MyDecorationType { get => myDecorationType; }
    public bool Upgrading { get => upgrading; set => upgrading = value; }
    public bool Completed { get => completed; }

    //constructor
    public Decoration(DecorationType decorationType, int points)
    {
        this.points = points;
        myDecorationType = decorationType;
        upgrading = false;
        completed = false;
    }

    //declared virtual so that it can be overriden if needed
    public virtual int GetPoints()
    {
        return points;
    }

    private bool upgrading = false;
    private bool completed = false;
    private float upgradeTimer = 0f;



    public void Upgrade(UpgradeMethod upgradeMethod)
    {
        if (upgrading)
        {
            switch (upgradeMethod)
            {
                case UpgradeMethod.timeBased:
                    TimeUpgrade();
                    break;
                case UpgradeMethod.buttonMash:
                    break;
                case UpgradeMethod.numOfUpgradeMethods:
                    break;
                default:
                    break;
            }
        }
    }
    private void TimeUpgrade()
    {
        upgradeTimer += Time.deltaTime;


        if (upgradeTimer >= 15f)
        {
            upgradeTimer = 15f;
            completed = true;
        }
    }
}
