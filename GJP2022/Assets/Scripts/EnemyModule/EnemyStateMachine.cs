using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : CharacterStateMachine
{
    #region ---Fields---
    [Header("StateMachine")]
    [SerializeField] private string stateName;
    [SerializeField] private EnemyStateBase currentState;

    [Header("Movement")]
    private Seeker seeker;
    private AIPath aiPath;
    #endregion ---Fields---

    public void SetState(EnemyStateBase state)
    {
        currentState = state;
        stateName = currentState.ToString();
    }

    private void Awake()
    {
        SetState(new EnemyDisableState(this));
        GameFlowManager.Singleton.OnGameStateChanged += HandleLevelStageChanged;
        seeker = GetComponent<Seeker>();
        aiPath = GetComponent<AIPath>();
    }

    private void Update()
    {
        currentState.UpdateState();
    }

    private void HandleLevelStageChanged(LevelStage stage)
    {
        if (stage == LevelStage.gameMode)
        {
            SetState(new MeleeEnemySearchState(this, seeker, aiPath));
        }
        if (stage == LevelStage.inbetween)
        {
            SetState(new EnemyDisableState(this));
        }
        if (stage == LevelStage.victory)
        {
            SetState(new EnemyDisableState(this));
        }
    }

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
        //if (stats.life <= 0)
        //{
        //    //TODO manage dead state
        //}
    }

    public override void AddDamage(Damage damageTaken)
    {
        base.AddDamage(damageTaken);
        //animator.Play(getHitHash);
    }
}
