using UnityEngine;
public class IdleState : IEnemyState
{
    private float _idleTime = 3f;
    private float _timer;
    
    Transform playerTransform;
    Transform enemyTransform;
    
    public void EnterState(EnemyManager enemy)
    {
        Debug.Log(this + "에서 나갔다");
        playerTransform = enemy.playerTransform;
        enemyTransform = enemy.transform;
        _timer = 0f;
        enemy.animator.Play("Idle");
    }

    public void ExitState(EnemyManager enemy)
    {
        Debug.Log(this + "에서 나갔다");
    }

    public void UpdateState(EnemyManager enemy)
    {
        if (Vector2.Distance(playerTransform.position, enemyTransform.position) < 8f)
        {
            enemy.TransitionToState(new ChaseState());
        }
        
        if (_idleTime < _timer)
        {
            enemy.TransitionToState(new PatrolState());
        }
        
        _timer += Time.deltaTime;
    }
}
