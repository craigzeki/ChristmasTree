using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaubleDecoration : Decoration
{

    private int bonusMultiplier = 2;
    public BaubleDecoration(int points, int bonusMultiplier, UpgradeMethod upgradeMethod) : base(DecorationType.Bauble, points, upgradeMethod)
    {
        this.bonusMultiplier = bonusMultiplier;
    }

    public override int GetPoints()
    {
        return points * bonusMultiplier;
    }
}
