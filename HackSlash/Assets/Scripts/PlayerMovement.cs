using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    [Header("Assigning variables!")] 
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Transform _transform;
    [SerializeField] private Transform _groundPoint;
    [SerializeField] private LayerMask _groundLayer;

    
    [Header("Player characteristics!")] [SerializeField]
    private float _movementSpeed = 1f;
    [SerializeField] private float _jumpForce = 1f;
    [SerializeField] private float _groundCheckRadius = 1f;

    
    [Header("Attack!")] [SerializeField] private Transform _attackPoint;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private float _attackRadius = 0.5f;
    [SerializeField] private bool DrawAttackRange = true;



    [HideInInspector] public float HorizontalInput;
    private bool _facingRight;


    private void Update()
    {
        HorizontalInput = Input.GetAxisRaw("Horizontal");

        HandleJumping();
        HandleAttack();
        HandleFlipping();
    }

    private void FixedUpdate()
    {
        // Moving.
        var moveDir = new Vector2(HorizontalInput * _movementSpeed * Time.fixedDeltaTime, _rigidbody.velocity.y);

        _rigidbody.velocity = moveDir;
    }


    private void HandleJumping()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jump();
        }
    }

    private void Jump()
    {
        _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }


    private void HandleAttack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
            EventManager.InvokeOnAttackActions();
        }
    }

    private void Attack()
    {
        var hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRadius, _enemyLayer);
        foreach (var enemy in hitEnemies)
        {
            Debug.Log("Hit: " + enemy.name);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null || !DrawAttackRange)
            return;

        Gizmos.DrawSphere(_attackPoint.position, _attackRadius);
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
}