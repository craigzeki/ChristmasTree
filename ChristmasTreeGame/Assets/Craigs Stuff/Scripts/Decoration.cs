using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DecorationType : int
{
    Star = 0,
    Bow,
    Bauble,
    Gift,
    RawBauble,
    //New decorations here
    NumOfDecorations
}
public class Decoration
{

    private DecorationType myDecorationType;

    //protected so that the children classes can access
    protected int points = 0;

    public DecorationType MyDecorationType { get => myDecorationType; }

    //constructor
    public Decoration(DecorationType decorationType, int points)
    {
        this.points = points;
        myDecorationType = decorationType;
    }

    //declared virtual so that it can be overriden if needed
    public virtual int GetPoints()
    {
        return points;
    }
}
