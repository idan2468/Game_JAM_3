using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObject : MonoBehaviour
{
    [SerializeField] private float distance = 1f;
    [SerializeField] private LayerMask boxMask;

    GameObject box;
    private FixedJoint2D boxJoint;
    [SerializeField] private float hight;
    private Rigidbody2D _rigidbody2D;
    private PlayerMove _playerMove;

    // Use this for initialization
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerMove = GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.queriesStartInColliders = false;
        var dir = _playerMove.IsFacingLeft ? Vector2.left : Vector2.right;
        RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.up * hight, dir * transform.localScale.x,
            distance);
        if (hit.collider != null && Input.GetButtonDown("Interact"))
        {
            box = hit.collider.gameObject;
            boxJoint = box.GetComponent<FixedJoint2D>();
            boxJoint.connectedBody = _rigidbody2D;
            boxJoint.enabled = true;
            // box.GetComponent<boxpull> ().beingPushed = true;
        }
        else if (Input.GetKeyUp(KeyCode.E) && box != null)
        {
            box = null;
            boxJoint.enabled = false;
            boxJoint = null;
            // box.GetComponent<boxpull> ().beingPushed = false;
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