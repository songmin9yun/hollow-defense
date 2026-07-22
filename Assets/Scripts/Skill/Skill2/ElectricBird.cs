using UnityEngine;
using System.Collections;

public class ElectricBird : MonoBehaviour
{
    [SerializeField] private Transform electricBirdPos;
    [SerializeField] GameObject electricPrefab;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] EnemyDataManager dataManager;
    
    [SerializeField] private float eletricForce;
    public bool getEletricAbility = true;

    public void EletricBirds()
    {
        if (!getEletricAbility)
        {
            return;
        }
        
        GameObject eletricObj = Instantiate(electricPrefab, electricBirdPos.position, electricBirdPos.rotation);
        
        Rigidbody2D eletricRb = eletricObj.GetComponent<Rigidbody2D>();
        
        eletricRb.AddForce(electricBirdPos.right * eletricForce, ForceMode2D.Impulse);
        
        StartCoroutine(WaitThreeSeconds(5));
    }
    
    public IEnumerator WaitThreeSeconds(int second)
    {
        getEletricAbility = false;
        yield return new WaitForSeconds(second);
        getEletricAbility = true;
    }
}
