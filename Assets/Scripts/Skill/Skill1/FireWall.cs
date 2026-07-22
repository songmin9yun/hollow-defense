using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class FireWall : MonoBehaviour
{
    [SerializeField] private Transform firePos;
    [SerializeField] GameObject firePrefab;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] EnemyDataManager dataManager;
    
    
    [SerializeField] private float fireForce;
    public bool getFireAbility = true;

    public void FireBall()
    {
        if (!getFireAbility)
        {
            return;
        }
        
        GameObject fireObj = Instantiate(firePrefab, firePos.position, firePos.rotation);
        
        Rigidbody2D fireRb = fireObj.GetComponent<Rigidbody2D>();
        
        fireRb.AddForce(firePos.right * fireForce, ForceMode2D.Impulse);
        
        StartCoroutine(WaitThreeSeconds(10));
    }
    
    public IEnumerator WaitThreeSeconds(int second)
    {
        getFireAbility = false;
        yield return new WaitForSeconds(second);
        getFireAbility = true;
    }
}
