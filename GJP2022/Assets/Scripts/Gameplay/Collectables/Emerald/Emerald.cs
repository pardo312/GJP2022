using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emerald : CollectableControllerBase
{
    public void OnEnable()
    {
        m_shouldRotate = true;
    }

    private protected override void CollideWithPlayer(Collider player)
    {
        PlayerPrefs.SetFloat("smerald", PlayerPrefs.GetFloat("smerald", 0) + Random.Range(1, 4));
        base.CollideWithPlayer(player);
    }
}
