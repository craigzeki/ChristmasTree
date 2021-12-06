using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RawBaubleDecoration : Decoration 
{

    private int bonusMultiplier = 2;
    public RawBaubleDecoration(int points, int bonusMultiplier) : base(DecorationType.RawBauble, points)
    {
        this.bonusMultiplier = bonusMultiplier;
    }

    public override int GetPoints()
    {
        return points * bonusMultiplier;
    }
}
