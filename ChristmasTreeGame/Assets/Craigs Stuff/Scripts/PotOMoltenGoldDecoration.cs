using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotOMoltenGoldDecoration : Decoration
{

    private int bonusMultiplier = 2;
    public PotOMoltenGoldDecoration(int points, int bonusMultiplier) : base(DecorationType.PotOMoltenGold, points)
    {
        this.bonusMultiplier = bonusMultiplier;
    }

    public override int GetPoints()
    {
        return points * bonusMultiplier;
    }
}
