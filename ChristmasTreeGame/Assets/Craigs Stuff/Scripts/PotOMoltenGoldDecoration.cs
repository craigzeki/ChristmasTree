using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotOMoltenGoldDecoration : Decoration
{

    private int bonusMultiplier = 2;
    public PotOMoltenGoldDecoration(int points, int bonusMultiplier, UpgradeMethod upgradeMethod) : base(DecorationType.PotOMoltenGold, points, upgradeMethod)
    {
        this.bonusMultiplier = bonusMultiplier;
    }

    public override int GetPoints()
    {
        return points * bonusMultiplier;
    }
}
