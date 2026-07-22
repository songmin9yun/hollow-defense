public interface IEnemyState
{
    void EnterState(EnemyManager enemy);
    void ExitState(EnemyManager enemy);
    void UpdateState(EnemyManager enemy);
}