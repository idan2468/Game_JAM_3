using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerMove : MonoBehaviour
{
    [Header("Params")] [SerializeField] private LayerMask platformLayerMask;
    [SerializeField] private float _speed;
    [SerializeField] private float _JumpSpeedX = 4f;
    [SerializeField] private float _jumpPower;
    [SerializeField] private float _jumpVolume = 0.2f;
    [SerializeField] private float isGroundedCastRadius = .2f;
    [SerializeField] private Transform isGroundedCastCenter;


    [Header("Debugging")] [SerializeField] private bool _canMove = false;
    [SerializeField] private bool facingLeft = false;
    [SerializeField] private Collider2D groundedCheckCollider;
    [SerializeField] private bool jump;

    [SerializeField] private bool _isGrounded;
    // Start is called before the first frame update

    [SerializeField] private PushObject _pushObject;
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float dirToMove;


    public bool CanMove
    {
        get => _canMove;
        set => _canMove = value;
    }

    public Rigidbody2D Rb => rb;

    public bool IsFacingLeft
    {
        get => facingLeft;
        set
        {
            facingLeft = value;
            transform.rotation = !facingLeft ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundedCheckCollider = GetComponentInChildren<EdgeCollider2D>();
        _pushObject = GetComponent<PushObject>();
        _animator = GetComponentInChildren<Animator>();
        _isGrounded = IsGrounded();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (_canMove)
        {
            dirToMove = Input.GetAxis("Horizontal");
            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
            }
        }

        if (_animator)
        {
            // if using dirToMove - the player will not play "walk" animation when not touching the controller
            _animator.SetFloat("speed", Mathf.Abs(rb.velocity.x));
            _animator.SetBool("isPushing", _pushObject.IsPushing);
        }
    }

    private void FixedUpdate()
    {
        if (!_canMove) return;
        var isGroundedNew = IsGrounded();
        if (isGroundedNew != _isGrounded)
        {
            _isGrounded = isGroundedNew;
            _animator.SetBool("isGrounded", _isGrounded);
        }

        var velocity = new Vector2(0, rb.velocity.y);
        if (jump && _isGrounded && !_pushObject.IsPushing)
        {
            velocity += Vector2.up * _jumpPower;
            MusicController.Instance.PlaySound("Jump" + Random.Range(1, 3), _jumpVolume);
        }

        jump = false;
        var dirTaken = Vector2.right * dirToMove;
        velocity = !IsGrounded() ? velocity + dirTaken * _JumpSpeedX : velocity + dirTaken * _speed;
        if (!_pushObject.IsPushing)
        {
            if ((facingLeft && velocity.x > 0.01f) || (!facingLeft && velocity.x < -0.01f))
            {
                IsFacingLeft = !IsFacingLeft;
            }
        }

        rb.velocity = velocity;
    }

    public bool IsGrounded()
    {
        var colliders =
            Physics2D.OverlapCircleAll(isGroundedCastCenter.position, isGroundedCastRadius, platformLayerMask);
        foreach (var colliderHit in colliders)
        {
            if (colliderHit.gameObject != gameObject)
            {
                return true;
            }
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        if (isGroundedCastCenter == null) return;
        Gizmos.color = Color.black;
        Gizmos.DrawSphere(isGroundedCastCenter.position, isGroundedCastRadius);
    }
}