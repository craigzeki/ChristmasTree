using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iDecoration
{
    Decoration GetDecoration();
    void DestroyDecoration();

    void SetUpgrading(bool state);

    bool GetUpgradeComplete();

    UpgradeMethod GetUpgradeMethod();

    bool GetPlayerInArea();

    void SetPlayerInArea(bool player);

    Collider GetPlayerCollider();

    void SetPlayerCollider(Collider collider);

    float GetUpgradeProgress();
}
