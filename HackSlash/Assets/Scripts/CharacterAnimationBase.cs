using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class CharacterAnimationBase : MonoBehaviour
{
    protected Animator animator;
    private Rigidbody2D _rb;
    
    protected readonly int RunningParam = Animator.StringToHash("isRunning");
    protected readonly int AttackingParam = Animator.StringToHash("isAttacking");
    
    private bool _previousRunningState;
    private bool _previousAttackingState;


    protected virtual void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    protected void HandleRunAnimation(bool isAttacking)
    {
        var running = _rb.velocity.magnitude > 0f && !isAttacking;
        
        if (running != _previousRunningState)
        {
            animator.SetBool(RunningParam, running);
            _previousRunningState = running;
        }
    }

    protected void HandleAttackAnimation(bool isAttacking)
    {
        if (isAttacking != _previousAttackingState)
        {
            animator.SetBool(AttackingParam, isAttacking);
            _previousAttackingState = isAttacking;
        }
    }
    
    
    
}