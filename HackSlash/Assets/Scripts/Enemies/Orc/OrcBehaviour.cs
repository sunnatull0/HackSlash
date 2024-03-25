using UnityEngine;

namespace Enemies.Orc
{
    [RequireComponent(typeof(OrcAttack))]
    public class OrcBehaviour : Enemy
    {
        private OrcAttack _orcAttack;

        [HideInInspector] public bool PlayerDetected;
        [SerializeField] private Transform _raycastPosition;
        private const float RaycastOffset = 2f;

        [Header("Ground Check!")] 
        [SerializeField] private Transform _groundPoint;
        [SerializeField] private float _groundCheckRadius;
        [SerializeField] private LayerMask _groundLayer;

        // OVERRIDES.
        protected override void Start()
        {
            _orcAttack = GetComponent<OrcAttack>();

            base.Start();
        }

        protected override void MoveTowardsPlayer()
        {
            base.MoveTowardsPlayer();

            if (_orcAttack.AttackStarted)
            {
                StopEnemyMovement();
            }
        }

        protected override void FlipTowardsPlayer()
        {
            if (_orcAttack.AttackStarted)
                return;

            base.FlipTowardsPlayer();
        }


        private void Update()
        {
            if (!IsGrounded())
                Debug.Log("not");

            if (!_orcAttack.AttackStarted && isNearPlayer && PlayerDetected)
            {
                // Debug.Log("Start Attack!");
                _orcAttack.StartAttackSystem();
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


        public bool IsGrounded()
        {
            return Physics2D.OverlapCircle(_groundPoint.position, _groundCheckRadius, _groundLayer);
        }
    }
}