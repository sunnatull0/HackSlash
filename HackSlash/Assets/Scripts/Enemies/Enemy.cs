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


        // Movement.
        [SerializeField] protected float moveSpeed = 100f;
        [SerializeField] protected float _stopDistance = 0.5f;
        private Vector2 _moveDirection;
        protected bool isMovingRight;
        protected bool isNearPlayer;


        // Enemy components.
        protected Transform myTransform;
        private Rigidbody2D _rb;
        private Collider2D _myCollider;

        // Borders.
        private Collider2D _borderCollider;
        private const float BorderPositionX = 34f; // Border X position.
        protected bool IsOutsideOfBorders => Mathf.Abs(myTransform.position.x) > BorderPositionX;

        // Player.
        protected LayerMask playerLayer;
        private Transform _playerTransform;
        private const string PlayerName = "Player";


        protected virtual void Start()
        {
            _playerTransform = GameObject.Find(PlayerName).transform;
            playerLayer = LayerMask.GetMask(PlayerName);
            
            myTransform = GetComponent<Transform>();
            _rb = GetComponent<Rigidbody2D>();
            _myCollider = GetComponent<Collider2D>();

            SetMoveDirection();
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
            _moveDirection = isMovingRight ? Vector2.right : Vector2.left;

            _rb.velocity = _moveDirection * moveSpeed * Time.fixedDeltaTime;
        }


        protected virtual void MoveTowardsPlayer()
        {
            SetMoveDirection();
            isNearPlayer = Mathf.Abs(_moveDirection.x) < _stopDistance;
            if (isNearPlayer) // If enemy is near player, stop moving.
            {
                StopEnemyMovement();
                return;
            }

            _moveDirection.Normalize();
            _rb.velocity = _moveDirection * moveSpeed * Time.fixedDeltaTime;
        }

        protected void StopEnemyMovement()
        {
            _rb.velocity = Vector2.zero;
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
            var scale = myTransform.localScale;
            scale.x *= -1f;
            myTransform.localScale = scale;
            isMovingRight = !isMovingRight;
        }

        protected virtual void FlipTowardsPlayer()
        {
            if(isNearPlayer) // Do not flip, if player is near.
                return;
            
            float sign = isMovingRight ? 1f : -1f;

            if (Mathf.Sign(_moveDirection.x) != sign)
            {
                Flip();
            }
        }

        private void SetCorrectFlipping()
        {
            // All prefabs flipped to left, so we flip it only when instantiated on the right.
            if (myTransform.position.x > 0f)
                return;

            Flip();
        }

        private void SetMoveDirection()
        {
            _moveDirection = (_playerTransform.position - myTransform.position);
            _moveDirection.y = 0f;
        }
        
    }
}