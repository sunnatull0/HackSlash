using UnityEngine;

namespace Enemies.Orc
{
    [RequireComponent(typeof(OrcAttack))]
    public class OrcBehaviour : Enemy
    {
        private OrcAttack _orcAttack;

        [Header("Ground Check!")] [SerializeField]
        private Transform _groundPoint;

        [SerializeField] private float _groundCheckRadius;
        [SerializeField] private LayerMask _groundLayer;

        [SerializeField] private float _upValue;
        // OVERRIDES.
        protected override void Start()
        {
            _orcAttack = GetComponent<OrcAttack>();
            SFXManager.Instance.PlaySFX(SFXType.OrcSpawn);

            MoveUp(_upValue);
            base.Start();
        }

        private void MoveUp(float yAxis)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + yAxis);
            Debug.Log("Changed!");
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
            if (!_orcAttack.AttackStarted && IsNearPlayer() && _orcAttack.PlayerDetected)
            {
                _orcAttack.StartAttackSystem();
            }
        }


        public bool IsGrounded()
        {
            return Physics2D.OverlapCircle(_groundPoint.position, _groundCheckRadius, _groundLayer);
        }
    }
}