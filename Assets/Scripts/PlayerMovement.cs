using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private KeyBinding _keyBinding;
    private Rigidbody2D _rb;
    [SerializeField] private float speed = 8f;
    private Vector2 hor;

    [Header("Dash")]
    private float dashCooldown = 3f;
    private float dashduration = 0.2f;
    [SerializeField] private float dashPower = 8f;
    private bool candash = true;
    private bool isdashing = false;
    private float facing;
    
    [Header("Jump")]
    [SerializeField] private float jumpPower = 8f;
    /*[SerializeField] GameObject _gc;*/
    private bool isGrounded;
    
    [Header("SelectCards")]
    [SerializeField] private GameObject scanObject;
    [SerializeField] private CardManager cardManager;
    
    
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _keyBinding = GetComponent<KeyBinding>();
    }
    
    void FixedUpdate()
    {
        if (isdashing) return;
        hor = _keyBinding.Dir;
        
        if (hor.x != 0) facing = Mathf.Sign(hor.x);
        _rb.linearVelocityX = hor.x * speed;

        if (hor.x > 0) transform.localScale = new Vector3(1, 1, 1);
        else if (hor.x < 0) transform. localScale = new Vector3(-1, 1, 1);
        
        
        _rb.linearVelocity = new Vector2(hor.x * speed, _rb.linearVelocity.y);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_rb.position, 500, LayerMask.GetMask("Object"));
        float dis = 100;

        for (int i = 0; i < colliders.Length; i++)
        {
            if (Vector2.Distance(transform.position, colliders[i].transform.position) <= dis)
            {
                dis = Vector2.Distance(transform.position, colliders[i].transform.position);
                scanObject =  colliders[i].gameObject;
            }
            colliders[i].transform.GetChild(0).gameObject.SetActive(false);
        }

        if (dis > 2)
        {
            scanObject = null;
        }

        if (dis <= 2)
        {
            scanObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void Cards()
    {
        if (scanObject != null)
        {
            cardManager.cardGet();
            scanObject.SetActive(false);   
        }
    }

    public void Dashing()
    {
        if (candash)
        {
            StartCoroutine(Dash());
        }
    }

    public void Jumping()
    {
        if (isGrounded)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _rb.linearVelocity.y + jumpPower);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            isGrounded = true;
        }
    }
    
    IEnumerator Dash()
    {
        candash = false;
        isdashing = true;
        float g = _rb.gravityScale;
        _rb.gravityScale = 0f;
        _rb.linearVelocity = new Vector2(facing * dashPower, 0f);
        yield return new WaitForSeconds(dashduration);
        _rb.gravityScale = g;
        isdashing = false;
        yield return new WaitForSeconds(dashCooldown);
        candash = true;
    }
}
