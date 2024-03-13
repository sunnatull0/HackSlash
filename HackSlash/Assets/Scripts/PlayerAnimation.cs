using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private Animator _animator;
    private const string _runParameter = "isRunning";
    private const string _jumpParameter = "isJumping";
    private const string _attackParameter = "Attack";


    private void Start()
    {
        EventManager.OnAttack += HandleAttackingAnimation;
    }

    private void OnDisable()
    {
        EventManager.OnAttack -= HandleAttackingAnimation;
    }


    private void Update()
    {
        HandleRunningAnimation();
        HandleJumpingAnimation();
    }

    private void HandleRunningAnimation()
    {
        if (!_playerMovement.IsGrounded())
        {
            _animator.SetBool(_runParameter, false);
            return;
        }


        float horizontalInput = _playerMovement.HorizontalInput;
        bool isRunning = _animator.GetBool(_runParameter);


        if (horizontalInput != 0 && !isRunning)
        {
            _animator.SetBool(_runParameter, true);
        }
        else if (horizontalInput == 0 && isRunning)
        {
            _animator.SetBool(_runParameter, false);
        }
    }

    private void HandleJumpingAnimation()
    {
        bool isJumping = _animator.GetBool(_jumpParameter);

        if (!_playerMovement.IsGrounded() && !isJumping)
        {
            _animator.SetBool(_jumpParameter, true);
            Debug.Log("Jumping");
        }
        else if (_playerMovement.IsGrounded() && isJumping)
        {
            _animator.SetBool(_jumpParameter, false);
            Debug.Log("Stop");
        }
    }

    private void HandleAttackingAnimation()
    {
        if (!_playerMovement.IsGrounded())
            return;
        
        _animator.SetTrigger(_attackParameter);
    }
}