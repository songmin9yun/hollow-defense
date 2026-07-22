using UnityEngine;

[CreateAssetMenu(menuName = "Data/EnemyData")]
public class EnemyData : ScriptableObject
{
    public string EnemyName;
    [TextArea] public string EnemyDescription;
    public float MaxHP;
    public int Damage;
    public float attackCooldown;
    public float MoveSpeed;
}
