using System;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float moveSpeed;
    public float startY;

    [Header("참조")]
    public Rigidbody2D rb;
    public IEnemyState CurrentState;
    public EnemyDataManager enemyDataManager;
    public Animator animator;
    public Transform playerTransform;
    public LayerMask playerLayer;
    public Timer timer;
    public Flip flips;
    public Timer time;


    [Header("지면 감지 설정")]
    public float rayDistance = 3f;

    public LayerMask groundLayer;

    [Header("상태 확인")]
    public bool isGrounded;

    void Start()
    {
        TransitionToState(new IdleState());
        startY = transform.position.y;
    }
    
    void Update()
    {
        if (enemyDataManager.isDying)
        {
            transform.position = new Vector2(transform.position.x, startY);
            GetComponent<Collider2D>().enabled = false;
            enemyDataManager.ItemDrop(transform.position);
            enemyDataManager.StartCoroutine(enemyDataManager.Dead());
            return;
        }

        if (enemyDataManager.knockBack.isKnockedBack)
        {
            enemyDataManager.knockBack.knockBack();
            return;
        }
        
        CurrentState.UpdateState(this);
        
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, rayDistance, groundLayer);
        
        if (time._timer < 3)
        {
            return;
        }
        
        if(!isGrounded && flips.dir > 0)
        {
            flips.leftFliping();
            time._timer = 0;
        }
        else if(!isGrounded &&  flips.dir < 0)
        {
            flips.rightFliping();
            time._timer = 0;
        }
    }

    public void TransitionToState(IEnemyState newState)
    {
        CurrentState?.ExitState(this);
        CurrentState = newState;
        CurrentState?.EnterState(this);
    }
}
