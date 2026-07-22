using System;
using UnityEngine;

public class FireTriiger : MonoBehaviour
{
    public int fireDamage = 20;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyDataManager enemy = collision.GetComponent<EnemyDataManager>();
        if (collision.CompareTag("Enemy"))
        {
            if (enemy != null)
            {
                enemy.TakeDamage(fireDamage);
                Destroy(gameObject);
            }
        }

        // if (enemy.time._timer > 3f)
        // {
        //     Destroy(gameObject);
        // }
    }

    public void Destorying()
    {
        GameObject.Destroy(gameObject);
    }
}
