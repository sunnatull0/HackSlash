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

    [Header("Player characteristics!")]
    [SerializeField] private float _movementSpeed = 1f;
    [SerializeField] private float _jumpForce = 1f;
    [SerializeField] private float _groundCheckRadius = 1f;
    [SerializeField] private KeyCode _buttonForJumping;

    private float _horizontalInput;
    private bool _facingRight = true;


    private void Update()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");

        
        HandleJumping();
        HandleFlipping(_horizontalInput);
    }

    private void FixedUpdate()
    {
        // Moving.
        var moveDir = new Vector2(_horizontalInput * _movementSpeed * Time.fixedDeltaTime, _rigidbody.velocity.y);
        _rigidbody.velocity = moveDir;
    }



    private void HandleJumping()
    {
        if (Input.GetKeyDown(_buttonForJumping) && IsGrounded())
        {
            Jump();
        }
    }
    
    private void Jump()
    {
        _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }
    
    
    
    private void HandleFlipping(float horizontalInput)
    {
        if (horizontalInput == 0f)
            return;
        if ((_facingRight && horizontalInput > 0) || !_facingRight && horizontalInput < 0)
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


    
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_groundPoint.position, _groundCheckRadius, _groundLayer);
    }
}