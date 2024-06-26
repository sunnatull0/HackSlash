using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerBehaviour))]
    public class PlayerAttack : MonoBehaviour
    {
        private PlayerBehaviour _playerBehaviour;

        [SerializeField] private Transform _attackPoint;
        [SerializeField] private LayerMask _enemyLayer;
        
        [HideInInspector] public bool _isAttacking;
        [SerializeField] private float _damage;
        [SerializeField] private float _attackDelay = 1f;
        [SerializeField] private float _attackRadius = 0.5f;
        [SerializeField] private bool DrawAttackRange = true;

        private float _nextAttackTime;
        private bool _wasAttacking;
        private bool _isAttackButtonPressed;


        private void Start()
        {
            _playerBehaviour = GetComponent<PlayerBehaviour>();
        }

        private void Update()
        {
            if (PauseControl.IsPaused) // If game is paused, stop all handling.
                return;

            if (!_playerBehaviour.BeingPushed)
            {
                HandleAttack();
            }

            HandleAttackFinish();
        }


        private void HandleAttack()
        {
            if ((Input.GetKeyDown(KeyCode.Space) || _isAttackButtonPressed) && _playerBehaviour.IsGrounded() &&
                CanAttack())
            {
                SFXManager.Instance.PlaySFX(SFXType.PlayerAttack);
                Attack(); // Attack
                EventManager.InvokeOnAttackActions();
            }

            _isAttackButtonPressed = false;
        }


        private void Attack()
        {
            _isAttacking = true;
            ExtendNextAttackTime();

            var hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRadius, _enemyLayer);
            foreach (var enemy in hitEnemies)
            {
                var enemyHealth = enemy.GetComponent<Health>();
                Damage(enemyHealth);
            }
        }


        private void ExtendNextAttackTime()
        {
            _nextAttackTime = Time.time + _attackDelay; // Delay handling
        }


        private void Damage(Health targetHealth)
        {
            targetHealth.TakeDamage(_damage);
        }


        private void HandleAttackFinish()
        {
            if (_wasAttacking && CanAttack())
            {
                FinishAttack();
            }

            _wasAttacking = !CanAttack();
        }


        public void FinishAttack()
        {
            _isAttacking = false;
            EventManager.InvokeOnAttackFinish();
        }


        private bool CanAttack()
        {
            return Time.time >= _nextAttackTime;
        }


        private void OnDrawGizmosSelected()
        {
            if (_attackPoint == null || !DrawAttackRange)
                return;

            Gizmos.DrawSphere(_attackPoint.position, _attackRadius);
        }


        public void OnAttackButtonPressed()
        {
            _isAttackButtonPressed = true;
        }
    }
}