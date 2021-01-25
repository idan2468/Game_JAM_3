using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TriggerZoomEnterEvent : MonoBehaviour
{
    private Sequence _animation;

    [SerializeField] private float timeOnCamera = 1f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other is EdgeCollider2D || !other.gameObject.CompareTag("Player")) return;
        _animation = DOTween.Sequence();
        _animation.AppendCallback(() =>
        {
            GameManager.Instance.ChangeVirtualCamera(GameManager.VirtualCamera.StairwayToHeaven);
            GameManager.Instance.FreezePlayer();
        });
        _animation.AppendInterval(GameManager.Instance.CameraBlendTime + timeOnCamera);
        _animation.AppendCallback(() => GameManager.Instance.ChangeVirtualCamera(GameManager.VirtualCamera.Main));
        _animation.AppendInterval(GameManager.Instance.CameraBlendTime);
        _animation.AppendCallback(() => GameManager.Instance.UnfreezePlayer());
    }
}