using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarDecoration : Decoration
{

    private int bonusMultiplier = 2;
    public StarDecoration(DecorationType decorationType, int points, int bonusMultiplier) : base(decorationType, points)
    {
        this.bonusMultiplier = bonusMultiplier;
    }

    public override int GetPoints()
    {
        return points * bonusMultiplier;
    }
}
