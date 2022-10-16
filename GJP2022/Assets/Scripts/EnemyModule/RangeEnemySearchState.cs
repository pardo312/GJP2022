using Pathfinding;
using System.Threading.Tasks;
using UnityEngine;
using Jiufen.Audio;

public class RangeEnemySearchState : EnemyStateBase
{
    Seeker seeker;
    AIPath aiPath;
    Path currentPath;
    EnemyStats enemyStats;

    public RangeEnemySearchState(EnemyStateMachine enemy, params object[] parameters) : base(enemy)
    {
        seeker = parameters[0] as Seeker;
        aiPath = parameters[1] as AIPath;
        enemyStats = parameters[2] as EnemyStats;
        timerBetweenAttacks = enemyStats.cooldownAttacks;
        SearchRandomPoint();
    }

    public void SearchRandomPoint()
    {
        if (searchingPlayer || currentPath != null || enemy == null)
            return;

        Vector2 randomCircle = Random.insideUnitCircle * 10;
        Vector3 randomSearch = enemy.transform.position + new Vector3(randomCircle.x, 0, randomCircle.y);
        currentPath = seeker.StartPath(enemy.transform.position, randomSearch, null);
    }

    public async void OnPathComplete()
    {
        currentPath = null;
        await Task.Delay(3000);
        SearchRandomPoint();
    }

    bool searchingPlayer = false;
    float timerBetweenAttacks;
    public override void UpdateState()
    {
        if (currentPath == null)
            return;
        Collider[] player = Physics.OverlapSphere(enemy.transform.position, 20, LayerMask.GetMask("Player"));

        if (player.Length > 0)
        {
            // --this could be in other state--
            Collider[] damageablePlayer = Physics.OverlapSphere(enemy.transform.position, 18, LayerMask.GetMask("Player"));
            seeker.CancelCurrentPathRequest();
            if (damageablePlayer.Length > 0)
            {
                if (!searchingPlayer)
                    AudioManager.PlayAudio("OST_FIGHT");

                aiPath.canMove = false;
                searchingPlayer = true;
                timerBetweenAttacks -= Time.deltaTime;
                if (timerBetweenAttacks < 0)
                {
                    enemy.InstantiateProjectile(damageablePlayer[0].transform.position - enemy.transform.position);
                    timerBetweenAttacks = enemyStats.cooldownAttacks;
                }
            }
            //-- --
            else
            {
                aiPath.canMove = true;
                currentPath = seeker.StartPath(enemy.transform.position, player[0].transform.position);
            }
        }
        else
        {
            if (searchingPlayer)
                AudioManager.PlayAudio("OST_WALK");
            timerBetweenAttacks = 0;
            if (aiPath.reachedEndOfPath)
                OnPathComplete();
            else if (searchingPlayer)
            {
                searchingPlayer = false;
                SearchRandomPoint();
            }
        }
    }
}
