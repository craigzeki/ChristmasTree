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
    MoltenStar,
    RawRibbon, 
    Ribbon,
    //New decorations here
    NumOfDecorations
}
public enum UpgradeMethod : int { NoMethod = 0, TimeBased, ButtonMash, AddTogether, RemoveFrom, NumOfUpgradeMethods }
public class Decoration
{

    

    private DecorationType myDecorationType;

    //protected so that the children classes can access
    protected int points = 0;

    public DecorationType MyDecorationType { get => myDecorationType; set => myDecorationType = value; }
    public bool Upgrading { get => upgrading; set => upgrading = value; }
    public bool Completed { get => completed; }
    public UpgradeMethod MyUpgradeMethod
    {
        get => myUpgradeMethod;
        set
        {
            myUpgradeMethod = value;
            completed = false;
            upgradeTimer = 0;
            progress = 0f;
        }
    }

    public bool PlayerInArea { get => playerInArea; set => playerInArea = value; }
    public Collider Collider { get => collider; set => collider = value; }
    public float Progress { get => progress; set => progress = value; }

    private UpgradeMethod myUpgradeMethod;

    //constructor
    public Decoration(DecorationType decorationType, int points, UpgradeMethod upgradeMethod)
    {
        this.points = points;
        myDecorationType = decorationType;
        upgrading = false;
        completed = false;
        myUpgradeMethod = upgradeMethod;
    }

    //declared virtual so that it can be overriden if needed
    public virtual int GetPoints()
    {
        return points;
    }

    private bool upgrading = false;
    private bool completed = false;
    private float upgradeTimer = 0f;
    private float upgradeCompleteTime = 15f;

    private float tempTime = 0f;
    private int health = 0;
    private int hitAmount = 10;

    private float progress = 0f;
    

    private bool playerInArea;
    private Collider collider;

    public void Upgrade(UpgradeMethod upgradeMethod)
    {
        if (upgrading)
        {
            switch (upgradeMethod)
            {
                case UpgradeMethod.TimeBased:
                    TimeUpgrade();
                    break;
                case UpgradeMethod.ButtonMash:
                    ButtonUpgrade();
                    break;
                
                case UpgradeMethod.NoMethod:
                    break;
                case UpgradeMethod.AddTogether:
                    SimpleAddItemsTogether();
                    break;
                case UpgradeMethod.RemoveFrom:
                    SimpleRemoveFrom();
                    break;
                case UpgradeMethod.NumOfUpgradeMethods:
                    break;
                default:
                    break;
            }
        }
    }
    private void TimeUpgrade()
    {
        upgradeTimer += Time.deltaTime;

        progress = (upgradeTimer / upgradeCompleteTime) * 100;
        if(progress > 100)
        {
            progress = 100;
        }
        if (upgradeTimer >= upgradeCompleteTime)
        {
            upgradeTimer = upgradeCompleteTime;
            completed = true;
        }
    }
    
    private void ButtonUpgrade()
    {
        tempTime += Time.deltaTime;

        if(playerInArea && collider != null)
        {
            if (tempTime > 0.2f && collider.gameObject.GetComponent<PlayerMovement>().interactPressed)
            {
                tempTime = 0f;
                health++;
                Debug.Log("Ribbon Upgrading");
                progress = (health / hitAmount) * 100;
                if (health >= hitAmount)
                {
                    completed = true;
                    Debug.Log("Ribbon complete");
                }


            }
        }
        
    }

    private void SimpleAddItemsTogether()
    {
        completed = true;
        progress = 100;
    }

    private void SimpleRemoveFrom()
    {
        completed = true;
        progress = 100;
    }
}
