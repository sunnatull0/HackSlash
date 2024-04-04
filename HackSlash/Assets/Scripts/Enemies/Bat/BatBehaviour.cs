using UnityEngine;
using UnityEngine.Serialization;

namespace Enemies.Bat
{
    public class BatBehaviour : Enemy
    {
        [SerializeField] private float _verticalDistance = 1f;
        [SerializeField] private float _changeDirectionTime = 2f; // Time between movements in seconds
        [SerializeField] private float t = 0f;

        private float _upperPoint;
        private float _lowerPoint;
        private bool _isMovingUp = true;
        private float _movementTimer;

        protected override void Start()
        {
            base.Start();

            SetVerticalPoints();
        }

        private void SetVerticalPoints()
        {
            Vector3 position = myTransform.position;
            _upperPoint = position.y + _verticalDistance;
            _lowerPoint = position.y - _verticalDistance;
        }

        protected override void MoveLeftToRight()
        {
            base.MoveLeftToRight();

            MoveVertically();
        }

        private void MoveVertically()
        {
            _movementTimer += Time.deltaTime;

            if (_movementTimer >= _changeDirectionTime)
            {
                _movementTimer = 0f;

                ChangeVerticalDirection();
            }

            float targetY = _isMovingUp ? _upperPoint : _lowerPoint;

            var position = myTransform.position;
            Vector2 targetPos = new Vector2(position.x, targetY);
            position = Vector2.Lerp(position, targetPos, t);
            myTransform.position = position;
        }

        private void ChangeVerticalDirection()
        {
            _isMovingUp = !_isMovingUp;
        }
    }
}