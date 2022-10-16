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

    [Header("Resources")]
    public EnemyStats enemyStats;
    #endregion ---Fields---

    public void SetState(EnemyStateBase state)
    {
        currentState = state;
        stateName = currentState.ToString();
    }

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        aiPath = GetComponent<AIPath>();
        SetState(new MeleeEnemySearchState(this, seeker, aiPath, enemyStats));
    }

    public override void Update()
    {
        base.Update();
        currentState.UpdateState();
    }

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
        if (characterResources.health <= 0)
            SetState(new EnemyDisableState(this));
    }

    public override void AddDamage(Damage damageTaken)
    {
        base.AddDamage(damageTaken);
    }
}
