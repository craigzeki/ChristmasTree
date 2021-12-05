using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowDecoration : Decoration
{

    private int bonusMultiplier = 2;
    public BowDecoration(int points, int bonusMultiplier) : base(DecorationType.Bow, points)
    {
        this.bonusMultiplier = bonusMultiplier;
    }

    public override int GetPoints()
    {
        return points * bonusMultiplier;
    }
}
