using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarDecoration : Decoration
{

    private int bonusMultiplier = 2;
    public StarDecoration(int points, int bonusMultiplier, UpgradeMethod upgradeMethod) : base(DecorationType.Star, points, upgradeMethod)
    {
        this.bonusMultiplier = bonusMultiplier;
    }

    public override int GetPoints()
    {
        return points * bonusMultiplier;
    }
}
