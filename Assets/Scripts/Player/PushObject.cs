using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObject : MonoBehaviour
{
    [SerializeField] private float distance = 1f;
    [SerializeField] private LayerMask boxMask;

    PushableObject box;
    private FixedJoint2D boxJoint;
    [SerializeField] private float hight;
    private Rigidbody2D _rigidbody2D;
    private PlayerMove _playerMove;
    private bool isPushing;

    public bool IsPushing => isPushing;

    // Use this for initialization
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerMove = GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        HandlePushButtonMode();
    }

    private void HandlePushButtonMode()
    {
        Physics2D.queriesStartInColliders = false;
       
        var dir = _playerMove.IsFacingLeft ? Vector2.left : Vector2.right;
        dir *= transform.localScale.x;
        var origin = (Vector2) transform.position + Vector2.up * hight;
        var hit = Physics2D.Raycast(origin, dir, distance, boxMask);
        
        if (hit.collider != null && Input.GetButtonDown("Interact") && _playerMove.IsGrounded())
        {
            box = hit.collider.gameObject.GetComponent<PushableObject>();
            if (box == null) return;
            isPushing = true;
            box.BeingPushedSetter(isPushing,_rigidbody2D);
        }
        
        else if ((Input.GetKeyUp(KeyCode.E) && isPushing) || (!_playerMove.IsGrounded() && isPushing))
        {
            isPushing = false;
            box.BeingPushedSetter(isPushing,null);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        var loc = (Vector2) transform.position + Vector2.up * hight;
        var dir = Vector2.right;
        if (_playerMove != null)
        {
            dir = _playerMove.IsFacingLeft ? Vector2.left : Vector2.right;
        }

        Gizmos.DrawLine(loc, loc + dir * transform.localScale.x * distance);
    }
}