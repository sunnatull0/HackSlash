using UnityEngine;

namespace Enemies.DireBoar
{
    [RequireComponent(typeof(DireBoarAttack))]
    public class DireBoarBehaviour : Enemy
    {
        private DireBoarAttack _direBoarAttack;

        [SerializeField] private LayerMask _playerLayer;
        [SerializeField] private float _detectDistance = 2f;
        private Vector2 _raycastDir;
        private RaycastHit2D _hit;

        [HideInInspector] public float DefaultSpeed;
        [HideInInspector] public bool PlayerDetected;


        // OVERRIDES.
        protected override void Start()
        {
            _direBoarAttack = GetComponent<DireBoarAttack>();
            DefaultSpeed = moveSpeed;


            base.Start();
        }

        protected override void MoveLeftToRight()
        {
            base.MoveLeftToRight();

            if (PlayerDetected && !_direBoarAttack.AttackStarted)
                _direBoarAttack.StartAttackSystem();
        }

        protected override void Flip()
        {
            base.Flip();

            if (!IsOutsideOfBorders &&
                _direBoarAttack.AttackStarted) // Stop enemy if it run into border while attacking.
            {
                StopEnemy();
            }
        }


        
        private void Update()
        {
            if (!PlayerDetected)
                DetectPlayer();
        }


        private void DetectPlayer()
        {
            _raycastDir = isMovingRight ? myTransform.right : -myTransform.right;
            _hit = Physics2D.Raycast(myTransform.position, _raycastDir, _detectDistance, _playerLayer);

            PlayerDetected = _hit.collider != null;
        }


        public void ChangeSpeed(float value)
        {
            moveSpeed = value;
        }

        public void StopEnemy() // Used in AnimationEvent
        {
            ChangeSpeed(0f);
        }
    }
}