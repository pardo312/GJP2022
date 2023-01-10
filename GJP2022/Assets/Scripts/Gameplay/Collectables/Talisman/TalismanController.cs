using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalismanController : CollectableControllerBase
{
    public Action m_onGetTalisman;

    private protected override void CollideWithPlayer(Collider player)
    {
        base.CollideWithPlayer(player);
        m_onGetTalisman?.Invoke();
    }

    public void OnEnable()
    {
        m_shouldRotate = true;
    }
}
