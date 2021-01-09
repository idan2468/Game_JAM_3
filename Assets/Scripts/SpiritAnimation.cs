﻿using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpiritAnimation : MonoBehaviour
{
    public float duration = 1f;

    public float strength = 2f;
    private Sequence animation;
    private UIManger _uiManger;
    private float intervalTime;
    [Range(1, 5)] private float intervalRandomMin = 1f;
    private float intervalRandomMax = 5f;

    private Vector3 initialPos;
    private Vector3 initialScale;

    [SerializeField] private bool resetAnimation = false;

    // Start is called before the first frame update
    void Start()
    {
        _uiManger = FindObjectOfType<UIManger>();
        initialPos = transform.localPosition;
        initialScale = transform.localScale;
        CreateAnimation();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void CreateAnimation()
    {
        animation = DOTween.Sequence();
        animation.Append(gameObject.transform.DOShakeScale(duration, strength));
        animation.Join(gameObject.transform.DOShakePosition(duration, strength));
        animation.AppendCallback(() => intervalTime = Random.Range(intervalRandomMin, intervalRandomMax));
        animation.AppendInterval(intervalTime);
        animation.SetLoops(-1);
        animation.Play();
    }

    private void OnValidate()
    {
        if (resetAnimation)
        {
            Debug.Log("Reset Animation");
            transform.position = initialPos;
            transform.localScale = initialScale;
            animation.Kill();
            CreateAnimation();
            resetAnimation = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other is EdgeCollider2D) return;
        animation.Kill();
        _uiManger.AddScore();
        Destroy(gameObject);
    }
}