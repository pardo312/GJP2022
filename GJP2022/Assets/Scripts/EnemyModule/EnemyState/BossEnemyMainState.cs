using System.Threading.Tasks;
using UnityEngine;
public abstract class BossStateBase
{

    protected BossStateMachine enemy;

    public BossStateBase(BossStateMachine enemy, params object[] parameters)
    {
        this.enemy = enemy;
    }
    public abstract void UpdateState();
}

public class BossEnemyMainState : BossStateBase
{
    BossHandCollider leftHandColliders;
    BossHandCollider rightHandColliders;
    public BossEnemyMainState(BossStateMachine enemy, params object[] parameters) : base(enemy)
    {
        enemy.animator.SetTrigger("Scream");
        //Cinemachine target boss;

        leftHandColliders = parameters[0] as BossHandCollider;
        rightHandColliders = parameters[1] as BossHandCollider;

        leftHandColliders.playerHit += PlayerHit;
        rightHandColliders.playerHit += PlayerHit;

        leftHandColliders.damageBoss += DamageBoss;
        rightHandColliders.damageBoss += DamageBoss;
    }

    public void PlayerHit(Collider player)
    {
        IDamageable playerDamageable = player.GetComponent<IDamageable>();
        playerDamageable.AddDamage(new NormalDamage() { amount = 10, target = playerDamageable });
    }

    public void DamageBoss(Damage damage)
    {
        enemy.AddDamage(damage);
    }

    public const float maxTimerBetweenAttacks = 5;
    public float timerBetweenAttacks = maxTimerBetweenAttacks;
    public override void UpdateState()
    {
        if (isDownPunchSequence)
            return;
        timerBetweenAttacks -= Time.deltaTime;
        if (timerBetweenAttacks <= 0)
        {
            if (Random.value > .5f)
                enemy.animator.SetTrigger("Roundhouse");
            else
                DownPunchAnimation();
        }
    }

    public bool isDownPunchSequence = false;
    public async void DownPunchAnimation()
    {
        isDownPunchSequence = true;
        enemy.animator.SetTrigger("DownPunch");
        await Task.Delay(2000);
        enemy.animator.speed = 0;
        await Task.Delay(5000);
        enemy.animator.speed = 1;
        isDownPunchSequence = false;
    }
}
