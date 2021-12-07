using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftDecoration : Decoration
{

    private int bonusMultiplier = 2;
    public GiftDecoration(int points, int bonusMultiplier, UpgradeMethod upgradeMethod) : base(DecorationType.Gift, points, upgradeMethod)
    {
        this.bonusMultiplier = bonusMultiplier;
    }

    public override int GetPoints()
    {
        return points * bonusMultiplier;
    }
}
