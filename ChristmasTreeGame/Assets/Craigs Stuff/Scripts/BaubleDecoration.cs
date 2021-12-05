using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaubleDecoration : Decoration
{

    private int bonusMultiplier = 2;
    public BaubleDecoration(int points, int bonusMultiplier) : base(DecorationType.Bauble, points)
    {
        this.bonusMultiplier = bonusMultiplier;
    }

    public override int GetPoints()
    {
        return points * bonusMultiplier;
    }
}
