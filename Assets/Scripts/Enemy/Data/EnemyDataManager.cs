using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EnemyDataManager : MonoBehaviour
{
    [Header("참조")]
    public EnemyData enemyData;
    public PlayerState playerState;
    public EnemyManager enemyManger;
    public Timer time;
    public Image healthBar;
    public Image playerHealthBar;
    public KnockBack knockBack;
    
    [Header("변수")]
    public bool isDying;
    public float health;
    public float playerHealth;
    public GameObject deadThing;
    private bool poisoned = false;

    [Header("드랍할 아이템 Prefab")]
    public GameObject ItemPrefab;

    [Header("드랍 확률 (%)")]
    [Range(0f, 100f)]
    public float dropPercentage = 5f;
    public bool isDropping = false;

    public SceneLoader sceneLoader;


    private void Awake()
    {
        health = enemyData.MaxHP;
        playerHealth = playerState.maxHealth;
    }

    void OnEnable()
    {
        health = (int)enemyData.MaxHP;
        healthBar.fillAmount = 1;
        playerHealthBar.fillAmount = 1;
        isDying = false;
        knockBack.isKnockedBack = false;
    }

    public void TakeDamage(int damage)
    {
        if (isDying)
        {
            return;
        }
        if (health <= 0)
        {
            Debug.Log(health);
            enemyManger.animator.SetTrigger("isDying");
            isDying = true;
            return;
        }

        health -= damage;
        knockBack.isKnockedBack = true;
        healthBar.fillAmount = health / enemyData.MaxHP;
        enemyManger.animator.SetTrigger("isHurting");
    }

    public void enemyAttack(int damage)
    {
        playerHealth -= damage;
        playerHealthBar.fillAmount = playerHealth / playerState.maxHealth;
        enemyManger.animator.SetTrigger("isAttacking");

        time._timer = 0;
        if (playerState.maxHealth <= 0)
        {
            Debug.Log("플레이어가 죽었다!");
            sceneLoader.ToGameOverScene();
        }
    }

    public IEnumerator Dead()
    {
        yield return new WaitForSeconds(3);
        isDying = false;
        Destroy(deadThing);
    }

    public void ItemDrop(Vector2 position)
    {
        float randomValue = Random.Range(0f, 100f);
        isDropping = true;
        if (randomValue <= dropPercentage && !isDropping)
        {
            Instantiate(ItemPrefab, position, Quaternion.identity);
        }else Debug.Log("못 뽑으셨습니다");
    }
    
    
    public bool IsPoisoned()
    {
        return poisoned;
    }
    
    public void ApplyPoison(int damage, float duration)
    {
        if(poisoned)
        {
            damage *= 2;
        }
        StartCoroutine(PoisonDamage(damage,duration));
    }
    
    IEnumerator PoisonDamage(int damage, float duration)
    {
        poisoned=true;
        float timer = 0;

        while(timer < duration)
        {
            TakeDamage(damage);
            yield return new WaitForSeconds(1f);
            timer++;
        }
        poisoned=false;
    }
}
