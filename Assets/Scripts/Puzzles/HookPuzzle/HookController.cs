using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class HookController : MonoBehaviour
{
    private PlayerMove _playerMove;
    private Rigidbody2D _playerRB;
    private Transform playerTransform;
    private Transform grabStartPos;
    private bool playerHooked;
    // [SerializeField] private float rotationSpeed;
    [SerializeField] private Vector3 playerScale;
    [SerializeField] private DistanceJoint2D _playerJoint;
    [SerializeField] private Transform startHook;
    [SerializeField] private Vector2 jumpForceOnDisconnect;
    private Transform playerSpriteObject;
    private bool collectedInfo;

    // Start is called before the first frame update
    void Start()
    {
        collectedInfo = false;
        grabStartPos = transform.GetChild(0);
        startHook = transform.GetChild(1);
        playerHooked = false;
    }

    private void Update()
    {
        if (playerHooked)
        {
            if (Input.GetButtonDown("Jump"))
            {
                DisconnectPlayerFromHook();
                return;
            }

            Vector3 targetDir = (playerTransform.position - transform.position).normalized;
            float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;

            transform.eulerAngles = new Vector3(0, 0, angle + 90);
            playerTransform.eulerAngles = new Vector3(0, 0, angle + 90);
            ChangePlayerRotation();
        }
    }
    // Update is called once per frame

    // private void HandleHookMovement()
    // {
    //     var rotationDir = Input.GetAxis("Horizontal");
    //     var currentAngel = transform.eulerAngles.z;
    //     transform.eulerAngles = new Vector3(0, 0, currentAngel + rotationDir * rotationSpeed * Time.deltaTime);
    //     // hookRB.AddTorque(rotationDir * rotationSpeed);
    // }


    private void ConnectPlayerToHook()
    {
        playerHooked = true;
        playerScale = playerTransform.localScale;
        playerTransform.position = grabStartPos.position;
        _playerJoint.connectedAnchor = startHook.position;
        _playerJoint.enableCollision = true;
        _playerJoint.enabled = true;
    }

    private void ChangePlayerRotation()
    {
        var rotationYPlayer = 0;
        if (_playerMove.IsFacingLeft)
        {
            rotationYPlayer = 180;
        }

        if (playerHooked)
        {
            playerSpriteObject.localEulerAngles = Vector3.up * rotationYPlayer;
            return;
        }
        playerTransform.eulerAngles = Vector3.up * rotationYPlayer;
    }
    private void DisconnectPlayerFromHook()
    {
        playerHooked = false;
        _playerJoint.enabled = false;
        transform.DORotate(Vector3.zero, 1f);
        playerSpriteObject.localEulerAngles = Vector3.zero;
        ChangePlayerRotation();
        var forceToApply = _playerMove.IsFacingLeft ? jumpForceOnDisconnect * Vector2.left : jumpForceOnDisconnect;
        _playerRB.AddForce(forceToApply);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other is EdgeCollider2D) return;
        if (other.CompareTag("Player") && !playerHooked)
        {
            if(!collectedInfo)
            {
                playerTransform = other.gameObject.transform.parent;
                _playerMove = playerTransform.gameObject.GetComponent<PlayerMove>();
                _playerRB = playerTransform.gameObject.GetComponent<Rigidbody2D>();
                _playerJoint = playerTransform.gameObject.GetComponent<DistanceJoint2D>();
                playerSpriteObject = playerTransform.GetChild(0);
                collectedInfo = true;
            }
            ConnectPlayerToHook();
        }
    }
}