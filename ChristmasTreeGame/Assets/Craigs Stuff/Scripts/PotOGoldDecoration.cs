using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotOGoldDecoration : Decoration
{

    private int bonusMultiplier = 2;
    public PotOGoldDecoration(int points, int bonusMultiplier, UpgradeMethod upgradeMethod) : base(DecorationType.PotOGold, points, upgradeMethod)
    {
        this.bonusMultiplier = bonusMultiplier;
    }

    public override int GetPoints()
    {
        return points * bonusMultiplier;
    }
}
