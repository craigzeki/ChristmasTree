using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotOGoldDecoration : Decoration
{

    private int bonusMultiplier = 2;
    public PotOGoldDecoration(int points, int bonusMultiplier) : base(DecorationType.PotOGold, points)
    {
        this.bonusMultiplier = bonusMultiplier;
    }

    public override int GetPoints()
    {
        return points * bonusMultiplier;
    }
}
