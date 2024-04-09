using UnityEngine;

namespace Enemies.Bear
{
    [RequireComponent(typeof(BearAttack))]
    public class BearBehaviour : Enemy
    {
        private BearAttack _bearAttack;

        [SerializeField] private Transform _raycastPosition;


        // OVERRIDES.
        protected override void Start()
        {
            _bearAttack = GetComponent<BearAttack>();

            base.Start();
        }

        protected override void MoveTowardsPlayer()
        {
            if (_bearAttack.AttackStarted)
                return;

            base.MoveTowardsPlayer();
        }

        protected override void FlipTowardsPlayer()
        {
            if (_bearAttack.AttackStarted)
                return;

            base.FlipTowardsPlayer();
        }


        private void Update()
        {
            if (!_bearAttack.AttackStarted && IsNearPlayer() && _bearAttack.PlayerDetected)
            {
                _bearAttack.StartAttackSystem();
            }
        }
    }
}