using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBaseDecoration : Decoration
{

    private int bonusMultiplier = 2;
    public BoxBaseDecoration(int points, int bonusMultiplier, UpgradeMethod upgradeMethod) : base(DecorationType.BoxBase, points, upgradeMethod)
    {
        this.bonusMultiplier = bonusMultiplier;
    }

    public override int GetPoints()
    {
        return points * bonusMultiplier;
    }
}
