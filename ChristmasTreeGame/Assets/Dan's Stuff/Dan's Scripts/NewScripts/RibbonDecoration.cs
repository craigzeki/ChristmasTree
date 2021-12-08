using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RibbonDecoration : Decoration
{
    private int bonusMultiplier = 2;
    public RibbonDecoration(int points, int bonusMultiplier, UpgradeMethod upgradeMethod) : base(DecorationType.Ribbon, points, upgradeMethod)
    {
        this.bonusMultiplier = bonusMultiplier;
    }

    public override int GetPoints()
    {
        return points * bonusMultiplier;
    }
}
