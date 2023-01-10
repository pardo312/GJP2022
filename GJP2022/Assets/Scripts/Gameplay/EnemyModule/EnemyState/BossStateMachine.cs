using Jiufen.Audio;
using System.Collections.Generic;
using UnityEngine;

public class BossStateMachine : CharacterStateMachine
{
    #region ---Fields---
    [Header("StateMachine")]
    [SerializeField] private string stateName;
    [SerializeField] private BossStateBase currentState;
    [SerializeField] private GameObject muzzlePrefab;

    public List<BossHandCollider> listOfColliders;
    #endregion ---Fields---

    public void SetState(BossStateBase state)
    {
        currentState = state;
        stateName = currentState.ToString();
    }

    private void Start()
    {
        SetState(new BossEnemyMainState(this, listOfColliders[0], listOfColliders[1]));
    }

    public override void Update()
    {
        base.Update();
        currentState.UpdateState();
    }

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
        AudioManager.PlayAudio("SFX_HIT_2");
        Destroy(Instantiate(muzzlePrefab, this.transform.position, this.transform.rotation), 5);

        if (CharacterResources.health <= 0)
        {
            //GameFlowManager.Singleton.enemyZonesController.KillEnemy();
            Destroy(this.gameObject);
        }
    }

    public override void AddDamage(Damage damageTaken)
    {
        base.AddDamage(damageTaken);
    }
}
