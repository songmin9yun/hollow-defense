using UnityEngine;

public class GreatSword : Weapons
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius = 1.5f;
    [SerializeField] private float attackCooldown = 0.2f;
    [SerializeField] private int damage = 25;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Animator slashEffect;
    [SerializeField] private Animator swordAnime;
    [SerializeField] private Timer timer;
    [SerializeField] private PlayerState playerState;
    [SerializeField] private EnemyDataManager data;
    public override void Attack()
    {
        if (attackCooldown > timer._timer)
        {
            return;
        }
        
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayer);
        slashEffect.gameObject.SetActive(false);
        slashEffect.gameObject.SetActive(true);
        // 2. 범위 내에 걸린 모든 적에게 데미지 적용
        foreach (Collider2D enemyCollider in hitEnemies)
        {
            EnemyDataManager enemy = enemyCollider.GetComponent<EnemyDataManager>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            
            if(data == null) continue;
            
            int finalDamage =
                playerState.CalculateDamage(damage);

            if(playerState.poisonFang)
            {
                data.ApplyPoison(playerState.poisonDamage, playerState.poisonTime);
            }
            data.TakeDamage(finalDamage);
        }
        swordAnime.SetTrigger("isAttacking");
        timer._timer = 0;
    }
    
    public void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        // 선택 시 attackPoint 위치에 반지름 크기의 와이어 구체를 그려줌
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
