using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RawRibbonDecoration : Decoration
{
    private int bonusMultiplier = 2;
    public RawRibbonDecoration(int points, int bonusMultiplier, UpgradeMethod upgradeMethod) : base(DecorationType.RawRibbon, points, upgradeMethod)
    {
        this.bonusMultiplier = bonusMultiplier;
    }

    public override int GetPoints()
    {
        return points * bonusMultiplier;
    }
}
