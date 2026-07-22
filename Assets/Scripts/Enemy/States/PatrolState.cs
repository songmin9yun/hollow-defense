using UnityEngine;

public class PatrolState : IEnemyState
{
    private float _movingTime = 3f;
    private float _timer;
    private Rigidbody2D _rb;

    Transform playerTransform;
    Transform enemyTransform;
    private float attackRadius;
    private LayerMask playerLayer;
    
    public void EnterState(EnemyManager enemy)
    {
        _rb = enemy.GetComponent<Rigidbody2D>();
        playerTransform = enemy.playerTransform;
        enemyTransform = enemy.transform;
        _timer = 0f;
        enemy.animator.SetFloat("isRunning", 1);
    }

    public void ExitState(EnemyManager enemy)
    {
        Debug.Log(this + "에서 나갔다");
        enemy.animator.SetFloat("isRunning", 0);
    }

    public void UpdateState(EnemyManager enemy)
    {
        if (Vector2.Distance(playerTransform.position, enemyTransform.position) < 8f)
        {
            enemy.TransitionToState(new ChaseState());
        }
        if (_movingTime > _timer)
        {
            _rb.linearVelocity = new Vector2(enemy.flips.dir * enemy.moveSpeed, _rb.linearVelocity.y);
        }
        else
        {
            enemy.TransitionToState(new IdleState());
        }
        
        _timer += Time.deltaTime;
    }
}
