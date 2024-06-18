using Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PlayerAttack))]
    public class PlayerBehaviour : MonoBehaviour, ICharacter
    {
        private PlayerAttack _playerAttack;

        [Header("UI Buttons")] [SerializeField]
        private Button _leftButton;

        [SerializeField] private Button _rightButton;
        [SerializeField] private Button _jumpButton;

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
        private float _currentMagnitude;
        private float _previousMagnitude;
        private bool _isLeftPressed;
        private bool _isRightPressed;
        private bool _isJumpPressed;


        private void Start()
        {
            _playerAttack = GetComponent<PlayerAttack>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _transform = GetComponent<Transform>();
            _myCollider = GetComponent<Collider2D>();
        }


        private void Update()
        {
            HandleMoveSound();

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

            HorizontalInput = 0f;

            if (_isLeftPressed)
            {
                HorizontalInput = -1f;
            }
            else if (_isRightPressed)
            {
                HorizontalInput = 1f;
            }
            else
            {
                HorizontalInput = Input.GetAxisRaw("Horizontal");
            }
        }


        private void HandleMovement()
        {
            var moveDir = new Vector2(HorizontalInput * _movementSpeed * Time.fixedDeltaTime, _rigidbody.velocity.y);
            _rigidbody.velocity = moveDir;
        }


        private void HandleMoveSound()
        {
            if (!IsGrounded() || PauseControl.IsPaused)
            {
                SFXManager.Instance.StopLoopingSFX();
                _previousMagnitude = 0;
                return;
            }

            _currentMagnitude = _rigidbody.velocity.x;

            if (_currentMagnitude != 0 && _previousMagnitude == 0)
            {
                SFXManager.Instance.PlayLoopingSFX(SFXType.PlayerRun);
            }
            else if (_currentMagnitude == 0 && _previousMagnitude != 0)
            {
                SFXManager.Instance.StopLoopingSFX();
            }

            _previousMagnitude = _rigidbody.velocity.x;
        }


        private void HandleJumping()
        {
            if ((Input.GetKeyDown(KeyCode.W) || _isJumpPressed) && IsGrounded() && !_playerAttack._isAttacking)
            {
                SFXManager.Instance.PlaySFX(SFXType.PlayerJump);
                Jump();
                EventManager.InvokeOnJumpActions(); // Additional effects (animations)
            }

            // Reset jump flag
            _isJumpPressed = false;
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
                SFXManager.Instance.PlaySFX(SFXType.PlayerLand);
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
        
        
        public void OnLeftButtonPressed()
        {
            _isLeftPressed = true;
            _isRightPressed = false;
        }

        public void OnRightButtonPressed()
        {
            _isRightPressed = true;
            _isLeftPressed = false;
        }

        public void OnJumpButtonPressed()
        {
            _isJumpPressed = true;
        }

        public void OnLeftButtonReleased()
        {
            _isLeftPressed = false;
        }

        public void OnRightButtonReleased()
        {
            _isRightPressed = false;
        }
    }
}