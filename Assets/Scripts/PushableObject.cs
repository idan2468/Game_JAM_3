using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableObject : MonoBehaviour
{
    public float defaultMass;
    [SerializeField] private float imovableMass;
    [SerializeField] private bool beingPushed;
    float xPos;
    public int mode;

    private Rigidbody2D _rigidbody2D;
    private FixedJoint2D _fixedJoint;

    // Use this for initialization
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _fixedJoint = GetComponent<FixedJoint2D>();
        _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        _rigidbody2D.mass = 20f;
    }

    public void BeingPushedSetter(bool isPushed, Rigidbody2D playerRigidbody2D)
    {
        beingPushed = isPushed;
        // _rigidbody2D.bodyType = beingPushed ? RigidbodyType2D.Dynamic : RigidbodyType2D.Kinematic;
        _rigidbody2D.velocity = Vector2.zero;
        _fixedJoint.connectedBody = playerRigidbody2D;
        _fixedJoint.enabled = isPushed;
        _rigidbody2D.mass = beingPushed ? 1f : 20f;
        _rigidbody2D.gravityScale = beingPushed ? 1f : 30f;
    }
}