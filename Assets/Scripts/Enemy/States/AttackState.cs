using UnityEngine;

public class AttackState : IEnemyState
{
    private Transform playerTransform;
    private Transform enemyTransform;
    private float attackRadius = 3f;
    private float distance;
    
    public void EnterState(EnemyManager enemy)
    {
        playerTransform = enemy.playerTransform;
        enemyTransform = enemy.transform;
        Debug.Log(this + "에서 들어왔다");
    }

    public void ExitState(EnemyManager enemy)
    {
        Debug.Log(this + "에서 나갔다");
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
        
        if (enemy.enemyDataManager.enemyData.attackCooldown > enemy.time._timer) return;
        
        Collider2D[] playerInfo = Physics2D.OverlapCircleAll(enemy.transform.position, attackRadius, enemy.playerLayer);

        foreach (Collider2D player in playerInfo)
        {
            enemy.enemyDataManager.enemyAttack(enemy.enemyDataManager.enemyData.Damage);
            enemy.animator.SetTrigger("isAttacking");
        }
        
        if(distance > 10f) enemy.TransitionToState(new ChaseState());
    }
}
