using Jiufen.Audio;
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
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private GameObject muzzlePrefab;
    [SerializeField] private GameObject smerald;

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
            SetState(new RangeEnemySearchState(this, seeker, aiPath, enemyStats));
    }

    public override void Update()
    {
        base.Update();
        currentState.UpdateState();
    }

    public void InstantiateProjectile(Vector3 direction)
    {
        Projectile projectile = Instantiate(projectilePrefab, transform.position + new Vector3(0, 0.4f, 0), transform.rotation);
        projectile.Init(direction);
    }

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
        AudioManager.PlayAudio("SFX_HIT_2");
        Destroy(Instantiate(muzzlePrefab, this.transform.position, this.transform.rotation), 5);

        if (CharacterResources.health <= 0)
        {
            SetState(new EnemyDisableState(this));
            Instantiate(smerald).transform.position = transform.position;
            //Spawn Money
            Destroy(this.gameObject);
        }
    }

    public override void AddDamage(Damage damageTaken)
    {
        base.AddDamage(damageTaken);
    }
}
