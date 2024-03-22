using System;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Bear))]
    public class BearAnimation : MonoBehaviour
    {
        private Animator _animator;
        private Rigidbody2D _rb;

        private readonly int _isRunning = Animator.StringToHash("isRunning");
        private readonly int _isAttacking = Animator.StringToHash("isAttacking");

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            _animator.SetBool(_isRunning, _rb.velocity.magnitude > 0f);
        }
    }
}
