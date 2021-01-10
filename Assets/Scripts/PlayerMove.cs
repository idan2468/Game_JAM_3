using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerMove : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private LayerMask platformLayerMask;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpPower;
    [SerializeField] private float _jumpVolume = 0.2f;


    [Header("Debugging")]
    [SerializeField] private bool _canMove = false;
    [SerializeField] private bool facingLeft = false;
    [SerializeField] private Collider2D groundedCheckCollider;
    // Start is called before the first frame update

    private PushObject _pushObject;
    private Animator _animator;
    public Rigidbody2D rb;


    public bool CanMove
    {
        get { return _canMove; }
        set { _canMove = value; }
    }

    public bool IsFacingLeft => facingLeft;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundedCheckCollider = GetComponentInChildren<EdgeCollider2D>();
        _pushObject = GetComponent<PushObject>();
        _animator = GetComponentInChildren<Animator>();
    }
    //todo: Move all movement handlers to FixedUpdate
    // Update is called once per frame
    void Update()
    {
        if (_canMove)
        {
            if (Input.GetButtonDown("Jump"))
            {
                if (IsGrounded() && !_pushObject.IsPushing)
                {
                    rb.velocity = Vector2.up * _jumpPower;
                    MusicController.Instance.PlaySound("Jump"+Random.Range(1,3),_jumpVolume);
                }
            }
        }

        if (_animator)
        {
            _animator.SetFloat("speed", Mathf.Abs(rb.velocity.x));
            _animator.SetBool("isGrounded", IsGrounded());
            _animator.SetBool("isPushing", _pushObject.IsPushing);
        }
    }
    private void FixedUpdate()
    {
        if (!_canMove) return;
        var dirTaken = new Vector2(Input.GetAxis("Horizontal"), 0);
        var dirVelocity = dirTaken * _speed;
        if (!_pushObject.IsPushing)
        {
            if (facingLeft && dirVelocity.x > 0.01f)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            if (!facingLeft && dirVelocity.x < -0.01f)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }

        rb.velocity = new Vector2(dirVelocity.x, rb.velocity.y);
        if (dirVelocity != Vector2.zero && !_pushObject.IsPushing)
        {
            facingLeft = dirVelocity.x < 0;
        }
    }

    public bool IsGrounded()
    {
        return groundedCheckCollider.IsTouchingLayers(platformLayerMask);
    }
}