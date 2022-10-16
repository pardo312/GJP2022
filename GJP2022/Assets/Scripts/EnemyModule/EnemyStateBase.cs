public abstract class EnemyStateBase
{

    protected EnemyStateMachine enemy;

    public EnemyStateBase(EnemyStateMachine enemy, params object[] parameters)
    {
        this.enemy = enemy;
    }
    public abstract void UpdateState();
}
