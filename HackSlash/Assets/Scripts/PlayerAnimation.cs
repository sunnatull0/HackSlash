using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private Animator _animator;
    private const string RunParameter = "isRunning";
    private const string JumpParameter = "isJumping";
    private const string AttackParameter = "isAttacking";


    private void Start()
    {
        EventManager.OnAttack += PlayAttackingAnimation;
        EventManager.OnJump += PlayJumpingAnimation;
        EventManager.OnLand += ResetJumpAnimation;
        EventManager.OnAttackFinish += ResetAttackAnimation;
    }

    private void OnDisable()
    {
        EventManager.OnAttack -= PlayAttackingAnimation;
        EventManager.OnJump -= PlayJumpingAnimation;
        EventManager.OnLand -= ResetJumpAnimation;
        EventManager.OnAttackFinish -= ResetAttackAnimation;
    }


    private void Update()
    {
        PlayRunningAnimation();
    }

    private void PlayRunningAnimation()
    {
        if (!_playerMovement.IsGrounded())
        {
            _animator.SetBool(RunParameter, false);
            return;
        }


        float horizontalInput = _playerMovement.HorizontalInput;
        bool isRunning = _animator.GetBool(RunParameter);


        if (horizontalInput != 0 && !isRunning)
        {
            _animator.SetBool(RunParameter, true);
        }
        else if (horizontalInput == 0 && isRunning)
        {
            _animator.SetBool(RunParameter, false);
        }
    }

    private void PlayJumpingAnimation()
    {
        _animator.SetBool(JumpParameter, true);
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
    private void ResetAttackAnimation()
    {
        _animator.SetBool(AttackParameter, false);
    }
}