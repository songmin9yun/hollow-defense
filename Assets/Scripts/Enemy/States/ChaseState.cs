using Unity.VisualScripting;
using UnityEngine;

public class ChaseState : IEnemyState
{
    private Transform playerTransform;
    private Transform enemyTransform;
    private float distance;
    
    public void EnterState(EnemyManager enemy)
    {
        playerTransform = enemy.playerTransform;
        enemyTransform = enemy.transform;
        enemy.animator.SetFloat("isRunning", 1);
    }

    public void ExitState(EnemyManager enemy)
    {
        Debug.Log(this + "에서 나갔다");
        enemy.animator.SetFloat("isRunning", 0);
    }

    public void UpdateState(EnemyManager enemy)
    {
        distance = Vector2.Distance(playerTransform.position, enemyTransform.position);
        
        if (playerTransform.position.x > enemyTransform.position.x)
        {
            enemy.rb.linearVelocity = new Vector2(enemy.moveSpeed, enemy.rb.linearVelocity.y);
            enemy.flips.rightFliping();
        }
        else if (playerTransform.position.x < enemyTransform.position.x)
        {
            enemy.rb.linearVelocity = new Vector2(-enemy.moveSpeed, enemy.rb.linearVelocity.y);
            enemy.flips.leftFliping();
        }
        else
        {
            enemy.rb.linearVelocity = new Vector2(0, 0);
        }

        if (distance < 2f)
        {
            enemy.TransitionToState(new AttackState());
        }
        if (distance > 10f)
        {
            Debug.Log(distance);
            enemy.TransitionToState(new IdleState());
        }
    }
}
