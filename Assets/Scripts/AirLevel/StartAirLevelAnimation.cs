using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class StartAirLevelAnimation : MonoBehaviour
{
    private Sequence _animation;
    [Header("Params")] [SerializeField] private float fadeTime;
    [SerializeField] private Material fogBackMaterial;
    [SerializeField] private Material fogFrontMaterial;
    [SerializeField] private SpriteRenderer fogBack;
    [SerializeField] private SpriteRenderer fogFront;
    [SerializeField] private CinemachineCameraOffset mainCamera;
    [SerializeField] private float yOffsetFromCamera = -2f;
    [SerializeField] private Collider2D fallCollider;
    [Header("Debugging")]
    [SerializeField] private bool animationWasTriggered;
    [SerializeField] private float fogBackAlpha;
    [SerializeField] private float fogFrontAlpha;
    [SerializeField] private PlayerMove _playerMove;
    


    // Start is called before the first frame update
    void Start()
    {
        animationWasTriggered = false;
        if (fogBack == null || fogFront == null)
        {
            Debug.LogWarning("Missing Refs");
            return;
        }

        fogBackAlpha = fogBack.color.a;
        fogFrontAlpha = fogFront.color.a;
    }


    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (animationWasTriggered || !other.CompareTag("Player")) return;
        _playerMove = other.gameObject.GetComponentInParent<PlayerMove>();
        StartAnimation();
        animationWasTriggered = true;
    }

    private void StartAnimation()
    {
        _animation = DOTween.Sequence();
        _animation.AppendCallback(() =>
        {
            GameManager.Instance.FreezePlayer();
            _playerMove.IsFacingLeft = false;
            fallCollider.enabled = false;
        });
        _animation.Append(fogBack.DOFade(0, fadeTime));
        _animation.Join(fogFront.DOFade(0, fadeTime));
        _animation.AppendCallback(() =>
        {
            fogBack.material = fogBackMaterial;
            fogFront.material = fogFrontMaterial;
        });
        _animation.Append(fogBack.DOFade(fogBackAlpha, fadeTime));
        _animation.Join(fogFront.DOFade(fogFrontAlpha, fadeTime));
        _animation.Join(
            DOTween.To(() => mainCamera.m_Offset.y, (x) => mainCamera.m_Offset.y = x,
                yOffsetFromCamera, fadeTime));
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        GameManager.Instance.UnfreezePlayer();
        fallCollider.enabled = true;
    }
}