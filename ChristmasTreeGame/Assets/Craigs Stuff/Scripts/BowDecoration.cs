using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowDecoration : Decoration
{

    private int bonusMultiplier = 2;
    public BowDecoration(int points, int bonusMultiplier, UpgradeMethod upgradeMethod) : base(DecorationType.Bow, points, upgradeMethod)
    {
        this.bonusMultiplier = bonusMultiplier;
    }

    public override int GetPoints()
    {
        return points * bonusMultiplier;
    }
}
