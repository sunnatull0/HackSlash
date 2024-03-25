using UnityEngine;

namespace Enemies.Bear
{
    [RequireComponent(typeof(BearAttack))]
    public class BearBehaviour : Enemy
    {
        private BearAttack _bearAttack;

        [HideInInspector] public bool PlayerDetected;
        [SerializeField] private Transform _raycastPosition;
        private const float RaycastOffset = 2f;


        // OVERRIDES.
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

        protected override void FlipTowardsPlayer()
        {
            if (_bearAttack.AttackStarted)
                return;

            base.FlipTowardsPlayer();
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
            var hit = Physics2D.Raycast(_raycastPosition.position, raycastDir, _stopDistance + RaycastOffset,
                playerLayer);

            PlayerDetected = hit.collider != null;
        }
    }
}