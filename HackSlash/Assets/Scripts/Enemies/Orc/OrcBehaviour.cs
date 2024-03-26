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
            if (_orcAttack.AttackStarted)
                return;
            
            base.MoveTowardsPlayer();
        }

        protected override void FlipTowardsPlayer()
        {
            if (_orcAttack.AttackStarted)
                return;

            base.FlipTowardsPlayer();
        }


        private void Update()
        {
            if (!_orcAttack.AttackStarted && IsNearPlayer() && PlayerDetected)
            {
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
            var hit = Physics2D.Raycast(_raycastPosition.position, raycastDir, stopDistance + RaycastOffset,
                playerLayer);

            PlayerDetected = hit.collider != null;
        }


        public bool IsGrounded()
        {
            return Physics2D.OverlapCircle(_groundPoint.position, _groundCheckRadius, _groundLayer);
        }
    }
}