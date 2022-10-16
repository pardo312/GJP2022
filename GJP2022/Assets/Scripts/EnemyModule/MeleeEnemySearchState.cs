using Pathfinding;
using System.Threading.Tasks;
using UnityEngine;

public class MeleeEnemySearchState : EnemyStateBase
{
    Seeker seeker;
    AIPath aiPath;
    Path currentPath;

    public MeleeEnemySearchState(EnemyStateMachine enemy, params object[] parameters) : base(enemy)
    {
        seeker = parameters[0] as Seeker;
        aiPath = parameters[1] as AIPath;
        SearchRandomPoint();
    }

    public void SearchRandomPoint()
    {
        if (searchingPlayer || currentPath != null)
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
    public override void UpdateState()
    {
        if (currentPath == null)
            return;
        if (aiPath.reachedEndOfPath)
        {
            OnPathComplete();
            return;
        }
        Collider[] player = Physics.OverlapSphere(enemy.transform.position, 5, LayerMask.GetMask("Player"));

        if (player.Length > 0)
        {
            searchingPlayer = true;
            seeker.CancelCurrentPathRequest();
            currentPath = seeker.StartPath(enemy.transform.position, player[0].transform.position);
        }

        else if (searchingPlayer == true)
        {
            searchingPlayer = false;
            SearchRandomPoint();
        }
    }
}
