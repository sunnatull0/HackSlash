using System;
using System.Collections;
using Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemies
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Enemy : MonoBehaviour, ICharacter
    {
        private enum EnemyType
        {
            Stupid,
            Smart
        }

        [SerializeField] private EnemyType _enemyType;


        // Movement.
        [SerializeField] protected float moveSpeed = 100f;
        [SerializeField] protected float stopDistance = 5f;
        protected bool isMovingRight;

        // Enemy components.
        protected Transform myTransform;
        protected Rigidbody2D _rb;
        private Collider2D _myCollider;

        // Borders.
        private Collider2D _borderCollider;
        private const float BorderPositionX = 34f; // Border X position.
        protected bool IsOutsideOfBorders => Mathf.Abs(myTransform.position.x) > BorderPositionX;

        // Player.
        protected LayerMask playerLayer;
        protected Transform _playerTransform;
        private const string PlayerName = "Player";


        protected virtual void Start()
        {
            _playerTransform = GameObject.Find(PlayerName).transform;
            playerLayer = LayerMask.GetMask(PlayerName);

            myTransform = GetComponent<Transform>();
            _rb = GetComponent<Rigidbody2D>();
            _myCollider = GetComponent<Collider2D>();

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
            float direction = isMovingRight ? 1f : -1f;

            Vector2 moveDir = new Vector2(direction * moveSpeed * Time.fixedDeltaTime, _rb.velocity.y);
            _rb.velocity = moveDir;
        }


        protected virtual void MoveTowardsPlayer()
        {
            if (IsNearPlayer()) // If enemy is near player, stop moving.
            {
                StopMovement();
                return;
            }

            float direction = MathF.Sign(GetDirectionToPlayer().x); // Gets 1 or -1, depending on move direction.
            Vector2 moveDir = new Vector2(direction * moveSpeed * Time.fixedDeltaTime, _rb.velocity.y);
            _rb.velocity = moveDir;
        }

        protected bool IsNearPlayer()
        {
            return Mathf.Abs(GetDirectionToPlayer().x) < stopDistance;
        }


        protected void StopMovement()
        {
            _rb.velocity = new Vector2(0f, _rb.velocity.y); // Restrict horizontal movement.
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
            if (IsNearPlayer()) // Do not flip, if player is near.
                return;

            float sign = isMovingRight ? 1f : -1f;

            if (Mathf.Sign(GetDirectionToPlayer().x) != sign)
            {
                Flip();
            }
        }

        private void SetCorrectFlipping()
        {
            // All prefabs flipped to left, so we flip it only when instantiated on the left.
            // While instantiated on the right, flipping will be correct.
            if (myTransform.position.x > 0f)
                return;

            Flip();
        }


        private Vector2 GetDirectionToPlayer()
        {
            return _playerTransform.position - myTransform.position;
        }

        public Collider2D GetCollider()
        {
            return _myCollider;
        }
    }
}