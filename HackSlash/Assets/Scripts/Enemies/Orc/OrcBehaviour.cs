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
            if (!_orcAttack.AttackStarted && isNearPlayer && PlayerDetected)
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
            var hit = Physics2D.Raycast(_raycastPosition.position, raycastDir, _stopDistance + RaycastOffset,
                playerLayer);
    
            PlayerDetected = hit.collider != null;
        }
    
    }
}
