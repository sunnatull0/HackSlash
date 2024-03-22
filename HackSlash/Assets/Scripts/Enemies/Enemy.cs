using System.Collections;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Enemy : MonoBehaviour
    {
        private enum EnemyType
        {
            Stupid,
            Smart
        }

        [SerializeField] private EnemyType _enemyType;

        
        // Attack.
        [SerializeField] protected float health = 1f;
        [SerializeField] protected float damage = 1f;
        
        // Movement.
        [SerializeField] protected float moveSpeed = 100f;
        [SerializeField] protected float _stopDistance = 0.5f;
        private Vector2 _moveDirection;
        private Vector2 _previousMoveDirection;
        protected bool _isMovingRight;
        
        // Enemy components.
        protected Transform _transform;
        private Rigidbody2D _rb;
        private Collider2D _myCollider;

        // Borders.
        private Collider2D _borderCollider;
        private readonly float _borderPositionX = 34f; // Border X position.
        protected bool IsOutsideOfBorders => Mathf.Abs(_transform.position.x) > _borderPositionX;

        // Player transform.
        private Transform _playerTransform;
    
        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _playerTransform = GameObject.Find("Player").transform;
            _rb = GetComponent<Rigidbody2D>();
            _myCollider = GetComponent<Collider2D>();
        }

        protected virtual void Start()
        {
            SetMoveDirection();
            SetPreviousMoveDirection();
            SetCorrectFlipping();
        }

        protected virtual void FixedUpdate()
        {
            if (_enemyType == EnemyType.Smart)
            {
                MoveTowardsPlayer(); // SmartMovement.
                FlipTowardsPlayer(); // Flip towards player.
            }
            else
            {
                MoveLeftToRight(); // StupidMovement
            }
        }


        protected virtual void MoveLeftToRight()
        {
            _moveDirection = _isMovingRight ? Vector2.right : Vector2.left;

            _rb.velocity = _moveDirection * moveSpeed * Time.fixedDeltaTime;
        }


        private void MoveTowardsPlayer()
        {
            SetMoveDirection();
            if (Mathf.Abs(_moveDirection.x) < _stopDistance) // If enemy is near player, stop moving.
            {
                _rb.velocity = Vector2.zero;
                return;
            }

            _moveDirection.Normalize();
            _rb.velocity = _moveDirection * moveSpeed * Time.fixedDeltaTime;
        }


        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.transform.CompareTag("Border"))
                return;

            // Check if the border is crossed first time, as enemies instantiated outside the level
            // So they have to enter first, without detecting borders (flipping).
            if (IsOutsideOfBorders)
            {
                const float resetCollisionSeconds = 4f;

                _borderCollider = other.collider;
                Physics2D.IgnoreCollision(_myCollider, _borderCollider, true);
                StartCoroutine(ResetCollisionAfterTime(resetCollisionSeconds));
                return;
            }

            //Flip back if it is stupid enemy.
            if (_enemyType == EnemyType.Stupid && !IsOutsideOfBorders)
            {
                Flip();
            }
        }

        private IEnumerator ResetCollisionAfterTime(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            Physics2D.IgnoreCollision(_myCollider, _borderCollider, false);
        }


        protected virtual void Flip()
        {
            var scale = _transform.localScale;
            scale.x *= -1f;
            _transform.localScale = scale;
            _isMovingRight = !_isMovingRight;
        }

        private void FlipTowardsPlayer()
        {
            if (Mathf.Sign(_previousMoveDirection.x) != Mathf.Sign(_moveDirection.x))
            {
                Flip();
            }

            SetPreviousMoveDirection();
        }

        private void SetCorrectFlipping()
        {
            // All prefabs flipped to left, so we flip it only when instantiated on the right.
            if (_transform.position.x > 0f)
                return;

            Flip();
        }

        private void SetMoveDirection()
        {
            _moveDirection = (_playerTransform.position - _transform.position);
            _moveDirection.y = 0f;
        }

        private void SetPreviousMoveDirection()
        {
            _previousMoveDirection = _moveDirection;
        }
    }
}