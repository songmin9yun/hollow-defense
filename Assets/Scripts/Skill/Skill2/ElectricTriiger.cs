using System.Collections;
using UnityEngine;

public class ElectricTriiger : MonoBehaviour
{
    public int electricDamage = 20;
    private float enemySpeed;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyDataManager enemy = collision.GetComponent<EnemyDataManager>();
        EnemyManager eM = collision.GetComponent<EnemyManager>();
        if (collision.CompareTag("Enemy"))
        {
            if (enemy != null)
            {
                enemy.TakeDamage(electricDamage);
                enemySpeed = enemy.enemyData.MoveSpeed;
                eM.moveSpeed = 2f;
                StartCoroutine(WaitThreeSeconds());
                eM.moveSpeed = enemySpeed;
            }
        }
    }

    public void Destorying()
    {
        GameObject.Destroy(gameObject);
    }
    
    IEnumerator WaitThreeSeconds()
    {
        yield return new WaitForSeconds(3.0f);
    }
}
