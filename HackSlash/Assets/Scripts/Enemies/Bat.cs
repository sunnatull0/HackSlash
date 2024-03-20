using UnityEngine;

namespace Enemies
{
    public class Bat : Enemy
    {
        
        [SerializeField] private float _verticalSpeed = 1f;
        [SerializeField] private float _verticalDistance = 1f;
        [SerializeField] private float _verticalDirectionChange = 0.2f;
        
        private float _upperPoint;
        private float _lowerPoint;
        private bool _isMovingUp = true;

        protected override void Start()
        {
            base.Start();

            Vector3 position = _transform.position;
            _upperPoint = position.y + _verticalDistance;
            _lowerPoint = position.y - _verticalDistance;
        }

        protected override void MoveLeftToRight()
        {
            base.MoveLeftToRight();
            
            float direction = _isMovingUp ? _upperPoint : _lowerPoint;

            Vector3 position = _transform.position;
            float newY = Mathf.Lerp(position.y, direction, Time.fixedDeltaTime * _verticalSpeed);
            position = new Vector3(position.x, newY, position.z);
            _transform.position = position;

            if (IsNearValue(newY, _upperPoint) || IsNearValue(newY, _lowerPoint))
            {
                ChangeVerticalDirection();
            }
        }

        private void ChangeVerticalDirection()
        {
            _isMovingUp = !_isMovingUp;
        }
        
        private bool IsNearValue(float value, float targetValue)
        {
            return Mathf.Abs(value - targetValue) < _verticalDirectionChange;
        }
        
    }
}