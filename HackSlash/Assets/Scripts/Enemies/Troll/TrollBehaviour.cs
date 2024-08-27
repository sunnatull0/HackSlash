using System.Collections;
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


        [SerializeField] private SFXType _spawnSound;
        protected override void Start()
        {
            SFXManager.Instance.PlaySFX(_spawnSound);
            
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


        private bool waiting;
        private void HandleSimpleAttack()
        {
            if (!waiting && !_trollAttack.AttackStarted && !_trollAttack.JumpAttackStarted && IsNearPlayer() && !_trollAttack.isWaiting && _trollAttack.PlayerDetected)
            {
                _trollAttack.StartAttackSystem();
                waiting = true;
                StartCoroutine(Testings());
            }
        }

        [SerializeField] private float _intervalBetweenSimpleAttacks = 2f;
        private IEnumerator Testings()
        {
            yield return new WaitForSeconds(_intervalBetweenSimpleAttacks);
            waiting = false;
        }

        private void HandleJumpAttack()
        {
            if (_trollAttack.CanJumpAttack() && !_trollAttack.AttackStarted && !_trollAttack.JumpAttackStarted && !_trollAttack.isWaiting)
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

        [SerializeField] private Animator _stampAnimator;
        [SerializeField] private TrollAnimation _trollAnimation;
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
            
            // CameraShake.
            var cameraShakeIntensity = 30f;
            var time = 0.2f;
            CameraShake.Instance.Shake(cameraShakeIntensity, time);
            _stampAnimator.SetTrigger("Stamp");
            
            _trollAnimation.ChangeColor();
        }

        [SerializeField] private SFXType _jumpAttackSound;
        private void Jump()
        {
            SFXManager.Instance.PlaySFX(_jumpAttackSound);
            _trollAttack.JumpAttackStarted = true;
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }


        private bool IsGrounded()
        {
            return Physics2D.OverlapCircle(_groundPoint.position, _groundCheckRadius, _groundLayer);
        }
    }
}