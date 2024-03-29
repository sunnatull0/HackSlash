using Enemies.Troll;
using Player;
using UnityEngine;

namespace Enemies.TrollBoss
{
    [RequireComponent(typeof(TrollBossAttack))]
    public class TrollBossBehaviour : Enemy
    {
        private TrollBossAttack _trollBossAttack;

        [SerializeField] private float _jumpForce;

        [SerializeField] private Transform _groundPoint;
        [SerializeField] private float _groundCheckRadius = 0.5f;
        [SerializeField] private LayerMask _groundLayer;
        private bool _wasGrounded;


        protected override void Start()
        {
            _trollBossAttack = GetComponent<TrollBossAttack>();
            _wasGrounded = true;
            Invoke(nameof(ActivateLanding),
                1f); // Activating landing after 1 second because enemy is landing when its created.

            base.Start();
        }

        protected override void MoveTowardsPlayer()
        {
            if (_trollBossAttack.AttackStarted || _trollBossAttack.JumpAttackStarted ||
                _trollBossAttack.isWaiting) // Do not move if enemy is attacking.
            {
                StopMovement();
                return;
            }

            base.MoveTowardsPlayer();
        }

        protected override void FlipTowardsPlayer()
        {
            if (_trollBossAttack.AttackStarted || _trollBossAttack.JumpAttackStarted ||
                _trollBossAttack.isWaiting) // Do not flip if enemy is attacking.
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
            if (!_trollBossAttack.AttackStarted && !_trollBossAttack.JumpAttackStarted && IsNearPlayer() &&
                !_trollBossAttack.isWaiting)
            {
                _trollBossAttack.StartAttackSystem();
            }
        }

        private void HandleJumpAttack()
        {
            if (_trollBossAttack.CanJumpAttack() && !_trollBossAttack.AttackStarted && !_trollBossAttack.JumpAttackStarted)
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
                // Damage.
                _trollBossAttack.JumpAttackDamage(playerBehaviour);
            }

            _trollBossAttack.FinishJumpAttack();
        }

        private void Jump()
        {
            _trollBossAttack.JumpAttackStarted = true;
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }


        private bool IsGrounded()
        {
            return Physics2D.OverlapCircle(_groundPoint.position, _groundCheckRadius, _groundLayer);
        }
    }
}