﻿using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class MovingInAirAnimation : MonoBehaviour
{
    [Header("Params")] [SerializeField] private float movementDist;
    [SerializeField] private float speed;
    [SerializeField] private bool restartAnimation;
    [SerializeField] private Ease easing = Ease.InOutSine;
    // [SerializeField] private LayerMask pushableObjectLayerMask;
    [Header("Debugging")]
    [SerializeField] private Vector3 startPos;
    private Tween _animation;
    private Transform previousParent;
   

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        StartAnimation();
    }

    private void StartAnimation()
    {
        restartAnimation = false;
        transform.position = new Vector3(startPos.x, startPos.y + movementDist / 2, startPos.z);
        SetAnimation();
    }

    private void OnValidate()
    {
        if (restartAnimation)
        {
            Debug.Log("Restarting animation");
            _animation.Kill();
            StartAnimation();
            Utility.DisableInspectorButton(() => restartAnimation = false).Play();
        }
    }

    private void SetAnimation()
    {
        _animation = transform.DOMoveY(transform.position.y - movementDist, movementDist / speed)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(easing).SetDelay(Random.Range(0f,1f));
    }

    // Update is called once per frame
    void Update()
    {
    }

    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     if (Utility.IsInLayerMask(other.gameObject, pushableObjectLayerMask))
    //     {
    //         
    //         other.gameObject.transform.parent = gameObject.transform;
    //     }
    //     
    // }
    //
    // private void OnCollisionExit2D(Collision2D other)
    // {
    //     if (Utility.IsInLayerMask(other.gameObject, pushableObjectLayerMask))
    //     {
    //         other.gameObject.transform.parent = transform.parent;
    //     }
    // }
}