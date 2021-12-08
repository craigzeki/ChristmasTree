using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxNoRibbonDecoration : Decoration
{

    private int bonusMultiplier = 2;
    public BoxNoRibbonDecoration(int points, int bonusMultiplier, UpgradeMethod upgradeMethod) : base(DecorationType.BoxNoRibbon, points, upgradeMethod)
    {
        this.bonusMultiplier = bonusMultiplier;
    }

    public override int GetPoints()
    {
        return points * bonusMultiplier;
    }
}
