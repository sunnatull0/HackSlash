using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerBehaviour))]
public class PlayerAnimation : MonoBehaviour
{
    
    private PlayerBehaviour _playerBehaviour;
    private Rigidbody2D _rb;
    private Animator _animator;
    private const string RunParameter = "isRunning";
    private const string JumpParameter = "isJumping";
    private const string AttackParameter = "isAttacking";
    private const string JumpAttackParameter = "isJumpAttacking";


    private void Awake()
    {
        _playerBehaviour = GetComponent<PlayerBehaviour>();
        _animator = GetComponentInChildren<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        EventManager.OnAttack += PlayAttackingAnimation;
        EventManager.OnJumpAttack += PlayJumpAttackingAnimation;
        EventManager.OnJump += PlayJumpingAnimation;
        EventManager.OnLand += OnLandActions;
        EventManager.OnAttackFinish += ResetAttackAnimation;
    }

    private void OnDisable()
    {
        EventManager.OnAttack -= PlayAttackingAnimation;
        EventManager.OnJumpAttack -= PlayJumpAttackingAnimation;
        EventManager.OnJump -= PlayJumpingAnimation;
        EventManager.OnLand -= OnLandActions;
        EventManager.OnAttackFinish -= ResetAttackAnimation;
    }


    private void Update()
    {
        PlayRunningAnimation();
    }

    private void OnLandActions()
    {
        ResetJumpAnimation();
        ResetJumpAttackingAnimation();
    }
    
    
    
    private void PlayRunningAnimation()
    {
        bool isRunning = _rb.velocity.magnitude > 0 && _playerBehaviour.IsGrounded();
        
        _animator.SetBool(RunParameter, isRunning);
    }

    private void PlayJumpingAnimation()
    {
        _animator.SetBool(JumpParameter, true);
    }

    private void PlayJumpAttackingAnimation()
    {
        ResetJumpAnimation();
        _animator.SetBool(JumpAttackParameter, true);
    }

    private void PlayAttackingAnimation()
    {
        //_animator.SetTrigger(AttackParameter);
        _animator.SetBool(AttackParameter, true);
    }


    private void ResetJumpAnimation()
    {
        _animator.SetBool(JumpParameter, false);
    }
    
    private void ResetJumpAttackingAnimation()
    {
        _animator.SetBool(JumpAttackParameter, false);
    }
    
    private void ResetAttackAnimation()
    {
        _animator.SetBool(AttackParameter, false);
    }
}