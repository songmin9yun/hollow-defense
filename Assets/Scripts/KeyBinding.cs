using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyBinding : MonoBehaviour
{
    // [Header("References")]
    // [SerializeField] private PlayerAttack Pa;
    [Header("Weapons")]
    [SerializeField] private Weapons currentWeapons;
    [SerializeField] private Weapons greatSword;
    
    [Header("Anything")]
    public Vector2 Dir { get; private set; }
    [SerializeField] InputActionReference jump;
    [SerializeField] InputActionReference bomb;
    [SerializeField] PlayerMovement _playerMovement;
    private SpawnBomb _sb;
    [SerializeField] Interaction interaction;
    [SerializeField] Animator animator;
    [SerializeField] PlayerState playerState;
    
    [Header("Effects")]
    [SerializeField] PollManager pollManager;
    [SerializeField] private Transform dashEffectPos;

    [Header("참조")]
    [SerializeField] private FireWall fireWall;
    [SerializeField] private ElectricBird electricBird;
    

    void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _sb = GetComponent<SpawnBomb>();
    }

    public void OnMove(InputValue value)
    {
        Dir = value.Get<Vector2>();
        if (Dir.x > 0)
        {
            animator.SetFloat("runSpeed", 1);
        }
        else if (Dir.x < 0)
        {
            animator.SetFloat("runSpeed", 1);
        }
        else if (Dir.x == 0)
        {
            animator.SetFloat("runSpeed", 0);
        }
    }

    public void OnDash(InputValue value)
    {
        if (value.isPressed)
        {
            _playerMovement.Dashing();
            animator.SetTrigger("isDashing");
            pollManager.SkillOn(dashEffectPos.position);
        }
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            _playerMovement.Jumping();
            animator.SetTrigger("isJumping");
        }
    }

    public void OnAttack(InputValue value)
    {
        if (value.isPressed)
        {
            currentWeapons.Attack();
            fireWall.FireBall();
            electricBird.EletricBirds();
        }
    }
    
    public void OnSkill(InputValue value)
    {
        if(value.isPressed)
        {
            playerState.SteamPressure();
        }
    }

    public void OnInteract(InputValue value)
    {
        if (value.isPressed)
        {
            Debug.Log("INteract");
            _playerMovement.Cards();
        }
    }

    /*void OnEnable()
    {
        _input.action.Enable();
        _input.action.performed += OnDash;
    }

    void OnDisable()
    {
        _input.action.Disable();
        _input.action.performed -= OnDash;
    }
    
    private void OnDash(InputAction.CallbackContext context)
    {
        _playerMovement.Dashing();
    }*/
}
