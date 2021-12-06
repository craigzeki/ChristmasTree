using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftDecoration : Decoration
{

    private int bonusMultiplier = 2;
    public GiftDecoration(int points, int bonusMultiplier) : base(DecorationType.Gift, points)
    {
        this.bonusMultiplier = bonusMultiplier;
    }

    public override int GetPoints()
    {
        return points * bonusMultiplier;
    }
}
