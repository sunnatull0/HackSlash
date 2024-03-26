using System;
using UnityEngine;

namespace Enemies.Troll
{
    [RequireComponent(typeof(TrollAttack))]
    public class TrollBehaviour : Enemy
    {
        private TrollAttack _trollAttack;
        
        [SerializeField] private float _jumpForce;
        private bool _wasGrounded;

        [SerializeField] private Transform _groundPoint;
        [SerializeField] private float _groundCheckRadius = 0.5f;
        [SerializeField] private LayerMask _groundLayer;

        protected override void Start()
        {
            _trollAttack = GetComponent<TrollAttack>();

            base.Start();
        }

        protected override void MoveTowardsPlayer()
        {
            if (_trollAttack.AttackStarted) // Do not move if enemy is attacking.
                return;

            base.MoveTowardsPlayer();
        }

        protected override void FlipTowardsPlayer()
        {
            if (_trollAttack.AttackStarted) // Do not flip if enemy is attacking.
                return;

            base.FlipTowardsPlayer();
        }



        private void Update()
        {
            HandleLanding();

            if (Input.GetKeyDown(KeyCode.V) && IsGrounded())
            {
                Jump();
            }

            if (!_trollAttack.AttackStarted && IsNearPlayer())
            {
                _trollAttack.StartAttackSystem();
            }
        }


        private void HandleLanding()
        {
            if (IsGrounded() && !_wasGrounded)
            {
                Land();
            }

            _wasGrounded = IsGrounded();
        }

        private void Jump()
        {
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }

        private void Land()
        {
            _trollAttack.JumpAttack();
        }

        private bool IsGrounded()
        {
            return Physics2D.OverlapCircle(_groundPoint.position, _groundCheckRadius, _groundLayer);
        }
    }
}