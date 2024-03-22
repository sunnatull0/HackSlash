using System;
using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DireBoarAnimation : MonoBehaviour
{

    private Animator _animator;
    private Rigidbody2D _rb;

    private const string RunningParameter = "isRunning";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_rb.velocity.magnitude > 0f)
        {
            _animator.SetBool(RunningParameter, true);
        }
        else
        {
            _animator.SetBool(RunningParameter, false);
        }
    }
    
    
    
}
