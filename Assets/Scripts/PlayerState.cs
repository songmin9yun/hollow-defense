using UnityEngine;
using System.Collections;

public class PlayerState : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 2000;



    [Header("Damage")]
    public float damageMultiplier = 1f;



    [Header("Attack Speed")]
    public float attackSpeedMultiplier = 1f;



    //=========================
    // 독사의 이빨
    //=========================

    public bool poisonFang = false;

    public int poisonDamage = 10;

    public float poisonTime = 3f;



    //=========================
    // 과열된 증기 발전기
    //=========================

    public bool steamGenerator = false;



    //=========================
    // 증기 압력
    //=========================

    public int steamStack = 0;

    private bool steamBuff = false;



    // 최종 데미지 계산

    public int CalculateDamage(int damage)
    {
        float value = damage;



        // 과열된 증기 발전기
        if(steamGenerator)
        {
            value = 1.1f;
        }



        // 증기 압력
        value= damageMultiplier;



        return Mathf.RoundToInt(value);
    }





    public void SteamPressure()
    {

        steamStack++;


        Debug.Log(
            "증기 압력 : " + steamStack);



        if(steamStack >= 5)
        {
            StartCoroutine(SteamBuff());
        }

    }




    IEnumerator SteamBuff()
    {

        if(steamBuff)
            yield break;



        steamBuff = true;



        damageMultiplier = 1.25f;

        attackSpeedMultiplier= 1.3f;



        Debug.Log("증기 과열!");



        yield return new WaitForSeconds(8f);



        damageMultiplier /= 1.25f;

        attackSpeedMultiplier /= 1.3f;



        steamStack = 0;


        steamBuff=false;



        Debug.Log("증기 종료");

    }
}
