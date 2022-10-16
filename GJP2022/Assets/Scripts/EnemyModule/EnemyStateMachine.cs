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
    //Delete this one as soon as possible, it could destroy worlds, even galaxies...
    [SerializeField] private bool isMelee;

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
        if (isMelee)
            SetState(new MeleeEnemySearchState(this, seeker, aiPath, enemyStats));
        else
            SetState(new MeleeEnemySearchState(this, seeker, aiPath, enemyStats));
    }

    public override void Update()
    {
        base.Update();
        currentState.UpdateState();
    }

    public void InstantiateProjectile(Transform parent, Vector3 direction)
    {
        //Instantiate(projectilePrefab, parent.position, parent.rotation);

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
