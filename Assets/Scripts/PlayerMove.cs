using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody2D rb;
    [SerializeField] private LayerMask platformLayerMask;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpPower;
    private int num;
    [SerializeField] private Collider2D myCollider; 
    // Start is called before the first frame update
    [SerializeField] private bool facingLeft = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myCollider = GetComponentInChildren<EdgeCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        { 
            Debug.Log("Here" + ++num);
            if(IsGrounded())
                rb.velocity = Vector2.up * _jumpPower;
        }
    }

    private void FixedUpdate()
    {
        var dirTaken = new Vector2(Input.GetAxis("Horizontal"), 0);
        var dirVelocity = dirTaken * _speed;
        if (facingLeft && dirVelocity.x > 0)
        {
            transform.rotation = Quaternion.Euler(0,0,0);
        }
        if (!facingLeft && dirVelocity.x < 0)
        {
            transform.rotation = Quaternion.Euler(0,180,0);
        }
        rb.velocity = new Vector2(dirVelocity.x,rb.velocity.y);
        if (dirVelocity != Vector2.zero)
        {
            facingLeft = dirVelocity.x < 0;    
        }
    }

    private bool IsGrounded()
    {
        return myCollider.IsTouchingLayers(platformLayerMask);
    }
}