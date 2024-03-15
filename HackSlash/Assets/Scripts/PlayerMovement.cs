using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    [Header("Assigning variables!")] 
    [SerializeField] private Rigidbody2D _rigidbody;

    [SerializeField] private Transform _transform;
    [SerializeField] private Transform _groundPoint;
    [SerializeField] private LayerMask _groundLayer;


    [Header("Player characteristics!")] 
    [SerializeField] private float _movementSpeed = 1f;

    [SerializeField] private float _jumpForce = 1f;
    [SerializeField] private float _groundCheckRadius = 1f;


    [Header("Attack!")] 
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private float _attackDelay = 1f;
    [SerializeField] private float _attackRadius = 0.5f;
    [SerializeField] private bool DrawAttackRange = true;


    [HideInInspector] public float HorizontalInput;
    private float _nextAttackTime = 0f;
    private bool _wasAttacking;
    private bool _isAttacking;
    private bool _facingRight;
    private bool _wasGrounded = true;


    
    private void Update()
    {
        HandleInput();
        HandleJumping();
        HandleLanding();
        HandleAttack();
        HandleAttackFinish();
        HandleFlipping();
    }

    private void FixedUpdate()
    {
        // Moving.
        HandleMovement();
    }


    private void HandleInput()
    {
        if (_isAttacking)
        {
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
        if (!Input.GetButtonDown("Jump") || !IsGrounded() || _isAttacking) 
            return;
        
        Jump();
        EventManager.InvokeOnJumpActions();
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
    }


    
    private void HandleAttack()
    {
        if (!Input.GetKeyDown(KeyCode.Space) || !IsGrounded() || !CanAttack())
            return;
        
        Attack();
        EventManager.InvokeOnAttackActions();
    }

    private void Attack()
    {
        var hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRadius, _enemyLayer);
        foreach (var enemy in hitEnemies)
        {
            //Debug.Log("Hit: " + enemy.name);
        }

        _nextAttackTime = Time.time + _attackDelay;
        _isAttacking = true;
    }
    
    private void HandleAttackFinish()
    {
        if (_wasAttacking && CanAttack())
        {
            FinishAttack();
        }

        _wasAttacking = !CanAttack();
    }

    private void FinishAttack()
    {
        _isAttacking = false;
        EventManager.InvokeOnAttackFinish();
    }
    

    private void HandleFlipping()
    {
        if (HorizontalInput == 0f)
            return;
        if ((_facingRight && HorizontalInput > 0) || !_facingRight && HorizontalInput < 0)
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
    
    
    private bool CanAttack()
    {
        return Time.time >= _nextAttackTime;
    }
    
    
    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null || !DrawAttackRange)
            return;

        Gizmos.DrawSphere(_attackPoint.position, _attackRadius);
    }
    
}
