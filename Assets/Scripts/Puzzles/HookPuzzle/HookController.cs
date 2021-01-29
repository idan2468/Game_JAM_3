using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class HookController : MonoBehaviour
{
    [Header("Params")] [SerializeField] private Transform startHook;
    [SerializeField] private Transform grabStartPos;
    [SerializeField] private Vector2 jumpForceOnDisconnect;
    [SerializeField] private EdgeCollider2D restrictAngleCollider;
    [SerializeField] private float delayEnableHookAfterDisconnect = 1f;
    [SerializeField] private bool catchFixedHigh = true;

    [Header("Debugging")] [SerializeField] private bool collectedInfo;
    [SerializeField] private PlayerMove _playerMove;
    [SerializeField] private Rigidbody2D _playerRB;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private bool playerHooked;
    [SerializeField] private Transform playerSpriteObject;
    [SerializeField] private DistanceJoint2D _playerJoint;
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private BoxCollider2D catchCollider;
    [SerializeField] private float diffAngle = 90f;
    [SerializeField] private bool canJumpFromHook = true;
    public bool CanJumpFromHook
    {
        get => canJumpFromHook;
        set => canJumpFromHook = value;
    }


    // Start is called before the first frame update
    void Start()
    {
        collectedInfo = false;
        playerHooked = false;
        catchCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (playerHooked)
        {
            if (Input.GetButtonDown("Jump") && canJumpFromHook)
            {
                DisconnectPlayerFromHook();
                return;
            }

            Vector3 targetDir = (playerTransform.position - startHook.position).normalized;
            float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
            angle += diffAngle;
            var newRotation = new Vector3(0, 0, angle);
            transform.eulerAngles = newRotation;
            playerTransform.eulerAngles = newRotation;
            // ChangePlayerRotation();
        }
    }

    private void ConnectPlayerToHook()
    {
        playerHooked = true;
        _playerAnimator.SetBool("isHooked", true);
        restrictAngleCollider.enabled = true;
        playerTransform.position = catchFixedHigh
            ? grabStartPos.position
            : new Vector3(grabStartPos.position.x, playerTransform.position.y, grabStartPos.position.z);
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

    private IEnumerator EnableHookAgainDelay()
    {
        catchCollider.enabled = false;
        yield return new WaitForSeconds(delayEnableHookAfterDisconnect);
        catchCollider.enabled = true;
    }

    private void DisconnectPlayerFromHook()
    {
        StartCoroutine(EnableHookAgainDelay());
        playerHooked = false;
        _playerAnimator.SetBool("isHooked", false);
        _playerJoint.enabled = false;
        restrictAngleCollider.enabled = false;
        transform.DORotate(Vector3.zero, 1f);
        playerSpriteObject.localEulerAngles = Vector3.zero;
        ChangePlayerRotation();
        var forceToApply = _playerMove.IsFacingLeft
            ? jumpForceOnDisconnect * (Vector2.left + Vector2.up)
            : jumpForceOnDisconnect;
        _playerRB.AddForce(forceToApply);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other is EdgeCollider2D) return;
        if (other.CompareTag("Player") && !playerHooked)
        {
            if (!collectedInfo)
            {
                playerTransform = other.gameObject.transform.parent;
                _playerMove = playerTransform.gameObject.GetComponent<PlayerMove>();
                _playerRB = playerTransform.gameObject.GetComponent<Rigidbody2D>();
                _playerJoint = playerTransform.gameObject.GetComponent<DistanceJoint2D>();
                playerSpriteObject = playerTransform.GetChild(0);
                _playerAnimator = playerSpriteObject.gameObject.GetComponent<Animator>();
                collectedInfo = true;
            }

            ConnectPlayerToHook();
        }
    }
}