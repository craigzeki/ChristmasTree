using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldDecoration : Decoration
{

    private int bonusMultiplier = 2;
    public GoldDecoration(int points, int bonusMultiplier, UpgradeMethod upgradeMethod) : base(DecorationType.Gold, points, upgradeMethod)
    {
        this.bonusMultiplier = bonusMultiplier;
    }

    public override int GetPoints()
    {
        return points * bonusMultiplier;
    }
}
