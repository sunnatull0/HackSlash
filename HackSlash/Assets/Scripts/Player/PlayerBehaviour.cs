using Interfaces;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PlayerAttack))]
    public class PlayerBehaviour : MonoBehaviour, ICharacter
    {
        private PlayerAttack _playerAttack;

        [Header("Ground variables!")] [SerializeField]
        private LayerMask _groundLayer;

        [SerializeField] private Transform _groundPoint;
        [SerializeField] private float _groundCheckRadius = 1f;

        [Header("Player characteristics!")] [SerializeField]
        private float _movementSpeed = 1f;

        [SerializeField] private float _jumpForce = 1f;

        [HideInInspector] public float HorizontalInput;
        [HideInInspector] public bool BeingPushed;
        private Rigidbody2D _rigidbody;
        private Transform _transform;
        private Collider2D _myCollider;
        private bool _facingRight;
        private bool _wasGrounded = true;


        private void Start()
        {
            _playerAttack = GetComponent<PlayerAttack>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _transform = GetComponent<Transform>();
            _myCollider = GetComponent<Collider2D>();
        }


        private void Update()
        {
            if (PauseControl.IsPaused) // If game is paused, stop all handling.
                return;

            HandleInput();
            HandleLanding();

            if (BeingPushed)
                return;

            // Function are stopped when player is being pushed away by enemies.
            HandleJumping();
            HandleFlipping();
        }

        private void FixedUpdate()
        {
            if (BeingPushed)
                return;

            // Moving.
            HandleMovement();
        }


        private void HandleInput()
        {
            if (IsAttackingInTheGround())
            {
                // Stop the movement
                HorizontalInput = 0f;
                return;
            }

            HorizontalInput = Input.GetAxisRaw("Horizontal");
        }

        private void HandleMovement()
        {
            var moveDir = new Vector2(HorizontalInput * _movementSpeed * Time.fixedDeltaTime, _rigidbody.velocity.y);
            _rigidbody.velocity = moveDir;
        }


        private void HandleJumping()
        {
            if (!Input.GetButtonDown("Jump") || !IsGrounded() || _playerAttack._isAttacking)
                return;

            Jump();
            EventManager.InvokeOnJumpActions(); // Additional effects (animations)
        }

        private void Jump()
        {
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }


        private void HandleLanding()
        {
            if (!_wasGrounded && IsGrounded())
            {
                // Landed.
                Land();
            }

            _wasGrounded = IsGrounded();
        }

        private void Land()
        {
            EventManager.InvokeOnLandActions();

            if (BeingPushed)
            {
                BeingPushed = false;
            }
        }


        private void HandleFlipping()
        {
            bool canFlip = (_facingRight && HorizontalInput < 0) || (!_facingRight && HorizontalInput > 0);

            if (HorizontalInput == 0f || !canFlip)
                return;

            Flip();
        }

        private void Flip()
        {
            var scale = _transform.localScale;
            scale.x *= -1f;
            _transform.localScale = scale;
            _facingRight = !_facingRight;
        }


        public bool IsGrounded()
        {
            return Physics2D.OverlapCircle(_groundPoint.position, _groundCheckRadius, _groundLayer);
        }


        private bool IsAttackingInTheGround()
        {
            return _playerAttack._isAttacking && IsGrounded();
        }


        public Collider2D GetCollider()
        {
            return _myCollider;
        }
    }
}