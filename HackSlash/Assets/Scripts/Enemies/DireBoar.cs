using System.Collections;
using UnityEngine;

namespace Enemies
{
    public class DireBoar : Enemy
    {
        [SerializeField] private LayerMask _playerLayer;
        [SerializeField] private float _detectDistance = 2f;
        [SerializeField] private float _attackSpeedMultiplier = 0.5f;
        [SerializeField] private float _delayBeforeAttack = 0.5f;
        [SerializeField] private float _attackTime = 0.5f;
        [SerializeField] private float _delayAfterAttack = 0.5f;
        private Vector2 _raycastDir;
        private RaycastHit2D _hit;
        private float _defaultSpeed;
        private bool _playerDetected;
        private bool _attackStarted;


        // OVERRIDES.
        protected override void Start()
        {
            base.Start();

            _defaultSpeed = moveSpeed;
        }
        
        protected override void MoveLeftToRight()
        {
            base.MoveLeftToRight();

            if (_playerDetected && !_attackStarted)
                StartAttack();
        }
        
        protected override void Flip()
        {
            base.Flip();
            
            if (!_firstTimeCrossingBorder && _attackStarted) // Stop enemy if it run into border while attacking.
            {
                StopEnemy();
            }
        }

        
        
        
        private void Update()
        {
            if (!_playerDetected)
                DetectPlayer();

            Debug.Log(moveSpeed + " " + _defaultSpeed);
        }


        
        private void DetectPlayer()
        {
            _raycastDir = _isMovingRight ? _transform.right : -_transform.right;
            _hit = Physics2D.Raycast(_transform.position, _raycastDir, _detectDistance, _playerLayer);

            _playerDetected = _hit.collider != null;
        }


        
        private void StartAttack()
        {
            if (_attackStarted) return;


            StopEnemy();
            StartCoroutine(StartAttackingAfterDelay());
            _attackStarted = true;
        }
        private IEnumerator StartAttackingAfterDelay()
        {
            yield return new WaitForSeconds(_delayBeforeAttack);

            Attacking();
        }

        
        
        private void Attacking()
        {
            ChangeSpeed(_defaultSpeed * _attackSpeedMultiplier); // Increase speed.

            StartCoroutine(StopAttackAfterDelay()); // Stop Attack.
        }
        private IEnumerator StopAttackAfterDelay()
        {
            yield return new WaitForSeconds(_attackTime);

            StopAttack();
        }

        
        
        private void StopAttack()
        {
            StopEnemy();

            StartCoroutine(ResetAttackAfterDelay()); // ResetAttack.
        }
        private IEnumerator ResetAttackAfterDelay()
        {
            yield return new WaitForSeconds(_delayAfterAttack);

            // Resetting.
            _playerDetected = false;
            _attackStarted = false;
            ChangeSpeed(_defaultSpeed);
        }
        
        

        private void ChangeSpeed(float value)
        {
            moveSpeed = value;
        }
        
        private void StopEnemy()
        {
            ChangeSpeed(0f);
        }

    }
}