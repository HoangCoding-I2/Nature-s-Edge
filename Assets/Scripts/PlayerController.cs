using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private PlayerControls _playerControls;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private Vector2 _movement;
    private SpriteRenderer _spriteRenderer;
    
    

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void Update()
    {
        PlayerInput();
        
    }

    private void FixedUpdate()
    {
        Move();
        HandleFlip();
    }

    private void PlayerInput()
    {
        _movement = _playerControls.Movement.Move.ReadValue<Vector2>();
        _animator.SetFloat("moveX", _movement.x);
        _animator.SetFloat("moveY", _movement.y);
    }
    
    private void Move()
    {
        _rigidbody2D.MovePosition(_rigidbody2D.position + _movement * (moveSpeed * Time.fixedDeltaTime));
    }
    
    private void HandleFlip()
    {
        var mousePos = Input.mousePosition;
        var playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if (mousePos.x < playerScreenPoint.x)
        {
            _spriteRenderer.flipX = true;
        }
        else
        {
            _spriteRenderer.flipX = false;
        }
        //_spriteRenderer.flipX = mousePos.x < playerScreenPoint.x;
    }
}
