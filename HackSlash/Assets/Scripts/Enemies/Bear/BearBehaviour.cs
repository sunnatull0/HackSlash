using UnityEngine;

namespace Enemies.Bear
{
    [RequireComponent(typeof(BearAttack))]
    public class BearBehaviour : Enemy
    {
        private BearAttack _bearAttack;

        [HideInInspector] public bool PlayerDetected;
        [SerializeField] private LayerMask _playerLayer;
        [SerializeField] private Transform _raycastPosition;
        private const float RaycastOffset = 2f;


        protected override void Start()
        {
            _bearAttack = GetComponent<BearAttack>();

            base.Start();
        }


        protected override void MoveTowardsPlayer()
        {
            base.MoveTowardsPlayer();

            if (_bearAttack.AttackStarted)
            {
                StopEnemyMovement();
            }
        }

        private void Update()
        {
            if (!_bearAttack.AttackStarted && isNearPlayer && PlayerDetected)
            {
                _bearAttack.StartAttackSystem();
            }
            
            
            if (!PlayerDetected)
            {
                DetectPlayer();
            }
        }


        private void DetectPlayer()
        {
            var raycastDir = isMovingRight ? myTransform.right : -myTransform.right;
            var hit = Physics2D.Raycast(_raycastPosition.position, raycastDir, _stopDistance + RaycastOffset, _playerLayer);

            PlayerDetected = hit.collider != null;
        }
    }
}