using System;
using Player;
using UnityEngine;

namespace Enemies.Troll
{
    [RequireComponent(typeof(TrollAttack))]
    public class TrollBehaviour : Enemy
    {
        private TrollAttack _trollAttack;

        [SerializeField] private float _jumpForce;

        [SerializeField] private Transform _groundPoint;
        [SerializeField] private float _groundCheckRadius = 0.5f;
        [SerializeField] private LayerMask _groundLayer;
        private bool _wasGrounded;


        protected override void Start()
        {
            _trollAttack = GetComponent<TrollAttack>();
            _wasGrounded = true;
            Invoke(nameof(ActivateLanding),
                1f); // Activating landing after 1 second because enemy is landing when its created.

            base.Start();
        }

        protected override void MoveTowardsPlayer()
        {
            if (_trollAttack.AttackStarted || _trollAttack.JumpAttackStarted ||
                _trollAttack.isWaiting) // Do not move if enemy is attacking.
            {
                StopMovement();
                return;
            }

            base.MoveTowardsPlayer();
        }

        protected override void FlipTowardsPlayer()
        {
            if (_trollAttack.AttackStarted || _trollAttack.JumpAttackStarted ||
                _trollAttack.isWaiting) // Do not flip if enemy is attacking.
                return;

            base.FlipTowardsPlayer();
        }


        private bool _landingActive;

        private void Update()
        {
            HandleJumpAttack();
            HandleSimpleAttack();
            if (_landingActive)
            {
                HandleLanding();
            }
        }


        private void HandleSimpleAttack()
        {
            if (!_trollAttack.AttackStarted && !_trollAttack.JumpAttackStarted && IsNearPlayer() && !_trollAttack.isWaiting && _trollAttack.PlayerDetected)
            {
                _trollAttack.StartAttackSystem();
            }
        }

        private void HandleJumpAttack()
        {
            if (_trollAttack.CanJumpAttack() && !_trollAttack.AttackStarted && !_trollAttack.JumpAttackStarted)
            {
                Jump(); // Starting JumpAttack system.
            }
        }

        private void ActivateLanding()
        {
            _landingActive = true;
        }

        private void HandleLanding()
        {
            if (IsGrounded() && !_wasGrounded)
            {
                OnLand();
            }

            _wasGrounded = IsGrounded();
        }

        private void OnLand()
        {
            var playerBehaviour = _playerTransform.GetComponent<PlayerBehaviour>();
            if (playerBehaviour.IsGrounded())
            {
                var playerHealth = _playerTransform.GetComponent<Health>();
                _trollAttack.JumpAttackDamage(playerHealth);
            }

            _trollAttack.StartWaiting();
            _trollAttack.FinishJumpAttack();
        }

        private void Jump()
        {
            _trollAttack.JumpAttackStarted = true;
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }


        private bool IsGrounded()
        {
            return Physics2D.OverlapCircle(_groundPoint.position, _groundCheckRadius, _groundLayer);
        }
    }
}