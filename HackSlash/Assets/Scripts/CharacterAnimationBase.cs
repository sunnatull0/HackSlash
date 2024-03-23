using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class CharacterAnimationBase : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _animator;
    
    protected readonly int RunningParam = Animator.StringToHash("isRunning");
    protected readonly int AttackingParam = Animator.StringToHash("isAttacking");
    
    private bool _previousRunningState;
    private bool _previousAttackingState;


    protected virtual void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    protected virtual void HandleRunAnimation(bool isAttacking)
    {
        var running = _rb.velocity.magnitude > 0f && !isAttacking;
        
        if (running != _previousRunningState)
        {
            _animator.SetBool(RunningParam, running);
            _previousRunningState = running;
        }
    }

    protected void HandleAttackAnimation(bool isAttacking)
    {
        if (isAttacking != _previousAttackingState)
        {
            _animator.SetBool(AttackingParam, isAttacking);
            _previousAttackingState = isAttacking;
        }
    }
    
    
    
}