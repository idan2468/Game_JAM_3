using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SpotLightEffect : MonoBehaviour
{
    // Start is called before the first frame update
    private SpriteRenderer spriteRenderer;
    private Tween _animation;

    [Header("Params")] [SerializeField] [Range(0, 1)]
    private float minOpacity;

    [SerializeField] [Range(0, 1)] private float maxOpacity = 1f;
    [SerializeField] private float fadeTime = 1f;
    [SerializeField] private Ease ease = Ease.InOutSine;
    [SerializeField] private bool resetAnimation;
    public Tween Animation => _animation;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetLightEffect();
    }

    private void SetLightEffect()
    {
        _animation = spriteRenderer.DOFade(minOpacity, fadeTime).From(maxOpacity).SetLoops(-1, LoopType.Yoyo)
            .SetEase(ease);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnValidate()
    {
        if (resetAnimation)
        {
            _animation.Kill();
            SetLightEffect();
            Utility.DisableInspectorButton(() => resetAnimation = false);
        }
    }

    private void OnDisable()
    {
        _animation.Kill();
    }

    public void FadeOutLight()
    {
        if (_animation.IsActive())
        {
            _animation.Kill();
        }

        _animation = spriteRenderer.DOFade(0, fadeTime)
            .SetEase(ease);
    }

    public void FadeInLight()
    {
        if (_animation.IsActive())
        {
            _animation.Kill();
        }
        _animation = spriteRenderer.DOFade(maxOpacity, fadeTime)
            .SetEase(ease).OnComplete(SetLightEffect);
    }
}