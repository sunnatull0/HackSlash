using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    
    [SerializeField] protected float health = 1f;
    [SerializeField] protected float moveSpeed = 100f;
    private Rigidbody2D _rb;
    private Vector2 _moveDirection;
    private Transform _transform;
    private bool _isMovingRight;
    private bool _firstTimeCrossingBorder = true;


    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        CheckFlipping();
    }

    private void FixedUpdate()
    {
        Move();
    }


    private void Move()
    {
        _moveDirection = _isMovingRight ? Vector2.right : Vector2.left;

        _rb.velocity = _moveDirection * moveSpeed * Time.fixedDeltaTime;
    }



    protected void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the border is crossed first time, as enemies instantiated outside the level
        // So they have to enter first, without detecting borders (flipping).
        if (!other.CompareTag("Border") || _firstTimeCrossingBorder)
        {
            _firstTimeCrossingBorder = false;
            return;
        }
        
        Flip();
    }

    private void Flip()
    {
        var scale = _transform.localScale;
        scale.x *= -1f;
        _transform.localScale = scale;
        _isMovingRight = !_isMovingRight;
    }

    private void CheckFlipping()
    {
        // All prefabs flipped to left, so we flip it only when instantiated on the right.
        if (_transform.position.x > 0f)
            return;

        Flip();
    }
}