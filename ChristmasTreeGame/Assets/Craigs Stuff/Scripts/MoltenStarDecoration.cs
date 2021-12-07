using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoltenStarDecoration : Decoration
{

    private int bonusMultiplier = 2;
    public MoltenStarDecoration(int points, int bonusMultiplier, UpgradeMethod upgradeMethod) : base(DecorationType.MoltenStar, points, upgradeMethod)
    {
        this.bonusMultiplier = bonusMultiplier;
    }

    public override int GetPoints()
    {
        return points * bonusMultiplier;
    }
}
