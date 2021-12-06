using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iDecoration
{
    Decoration GetDecoration();
    void DestroyDecoration();

    void SetUpgrading(bool state);

    bool GetUpgradeComplete();
}
