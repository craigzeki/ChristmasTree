using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxLidDecoration : Decoration
{

    private int bonusMultiplier = 2;
    public BoxLidDecoration(int points, int bonusMultiplier, UpgradeMethod upgradeMethod) : base(DecorationType.BoxLid, points, upgradeMethod)
    {
        this.bonusMultiplier = bonusMultiplier;
    }

    public override int GetPoints()
    {
        return points * bonusMultiplier;
    }
}
