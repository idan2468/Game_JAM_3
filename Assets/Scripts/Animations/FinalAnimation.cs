using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FinalAnimation : MonoBehaviour
{
    [SerializeField] private GameObject _targetTemple;
    [SerializeField] private float _ascentSpeed = 10f;
    [SerializeField] private Ease ease = Ease.InOutSine;
    private Sequence _animation;
    [SerializeField] private Transform playerTransform;
    private Collider2D[] playerColliders;

    // Start is called before the first frame update
    void Start()
    {
        playerColliders = GetComponentsInChildren<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        var dist = Vector3.Distance(playerTransform.position, _targetTemple.transform.position);
        _animation = 
            DOTween.Sequence()
                .AppendCallback(() =>
                {
                    GameManager.Instance.FreezePlayer();
                    foreach (var collider in playerColliders)
                    {
                        collider.enabled = false;
                    }
                    
                })
            .Append(playerTransform.DOMove(_targetTemple.transform.position, _ascentSpeed != 0 ? dist / _ascentSpeed : _ascentSpeed))
                .AppendCallback(() =>
                {
                    GameManager.Instance.UnfreezePlayer();
                    foreach (var collider in playerColliders)
                    {
                        collider.enabled = true;
                    }
                })
            .AppendCallback(() => playerTransform.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero)
            .SetEase(ease);
    }
    
}
