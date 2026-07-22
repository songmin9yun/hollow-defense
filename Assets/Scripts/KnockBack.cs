using System.Collections;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] float knockBackForce;
    [SerializeField] private Flip flips;
    public bool isKnockedBack;
    
    public void knockBack()
    {
        StartCoroutine(KnockBackRoutine());
    }

    public IEnumerator KnockBackRoutine()
    {
        isKnockedBack = true;
        _rb.AddForce(new Vector2(flips.dir * knockBackForce * -1, 0), ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.2f);
        isKnockedBack = false;
    }
}
