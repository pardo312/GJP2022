using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.ParticleSystem;

public class BarrierController : MonoBehaviour
{
    #region ----Fields----
    [Header("References")]
    public TalismanController m_talismanNeeded;
    public ParticleSystem m_backDropParticleSystem;
    private MainModule m_backDropPSMainModule;
    public ParticleSystem m_trailsParticleSystem;
    private MainModule m_trailsPSMainModule;

    [Header("Parameters")]
    public float m_timeFade = 2;
    private float m_timer = 0;
    private bool m_shouldFade = false;

    [Header("Events")]
    public UnityEvent m_onZoneUnlocked;
    #endregion ----Fields----

    #region ----Methods----
    private void Start()
    {
        m_backDropPSMainModule = m_backDropParticleSystem.main;
        m_trailsPSMainModule = m_trailsParticleSystem.main;
        m_talismanNeeded.m_onGetTalisman += OpenPortal;
    }

    private void OpenPortal()
    {
        m_shouldFade = true;
    }

    public void Update()
    {
        if (!m_shouldFade)
            return;

        if (m_timer <= m_timeFade)
        {
            m_timer += Time.deltaTime;
            Color c1 = m_backDropPSMainModule.startColor.color;
            m_backDropPSMainModule.startColor = new Color(c1.r, c1.g, c1.b, Mathf.Lerp(c1.a, 0, m_timer / m_timeFade));

            Color c2 = m_trailsPSMainModule.startColor.color;
            m_trailsPSMainModule.startColor = new Color(c2.r, c2.g, c2.b, Mathf.Lerp(c2.a, 0, m_timer / m_timeFade));
        }
        else
        {
            m_onZoneUnlocked?.Invoke();
            Destroy(this.gameObject);
        }
    }
    #endregion ----Methods----
}
