using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Random = UnityEngine.Random;

public class SpiritAnimation : MonoBehaviour
{
    private Vector3 initialPos;
    private Vector3 initialScale;

    [Header("Fade in/out animation params")] [SerializeField]
    private float fadeTime;

    private Sequence shakeAnimation;
    private Sequence fadeInAnimation;
    private Sequence fadeOutAnimation;
    private SpriteRenderer _spiritRenderer;
    private Light2D spiritLight;

    [Header("Shake animation params")] [SerializeField]
    float duration = 1f;

    [SerializeField] float strength = 2f;
    private float intervalTime;
    [Range(1, 5)] private float intervalRandomMin = 1f;
    private float intervalRandomMax = 5f;
    [SerializeField] private bool resetAnimation = false;
    [Header("Other")] [SerializeField] private ItemCollector _itemCollector;


    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.localPosition;
        initialScale = transform.localScale;
        _itemCollector = FindObjectOfType<ItemCollector>();
        _spiritRenderer = GetComponent<SpriteRenderer>();
        spiritLight = GetComponentInChildren<Light2D>();
        CreateAnimations();
        shakeAnimation.Play();
    }

    public Tween TriggerFadeInAnimation()
    {
        fadeInAnimation.Play();
        return fadeInAnimation;
    }

    public Tween TriggerFadeOutAnimation()
    {
        fadeOutAnimation.Play();
        return fadeOutAnimation;
    }

    private void CreateAnimations()
    {
        CreateShakeAnimation();
        CreateFadeInAnimation();
        CreateFadeOutAnimation();
    }

    private void CreateShakeAnimation()
    {
        shakeAnimation = DOTween.Sequence();
        shakeAnimation.Append(gameObject.transform.DOShakeScale(duration, strength));
        shakeAnimation.Join(gameObject.transform.DOShakePosition(duration, strength));
        shakeAnimation.AppendCallback(() => intervalTime = Random.Range(intervalRandomMin, intervalRandomMax));
        shakeAnimation.AppendInterval(intervalTime);
        shakeAnimation.SetLoops(-1);
        shakeAnimation.SetEase(Ease.InOutCubic);
    }

    private void CreateFadeInAnimation()
    {
        spiritLight.intensity = 0;
        fadeInAnimation = DOTween.Sequence();
        fadeInAnimation.Append(_spiritRenderer.DOFade(0, 0));
        fadeInAnimation.Append(DOTween.To(() => spiritLight.intensity,
            (x) => spiritLight.intensity = x, 1, fadeTime));
        fadeInAnimation.Join(_spiritRenderer.DOFade(1, fadeTime));
    }

    private void CreateFadeOutAnimation()
    {
        fadeOutAnimation = DOTween.Sequence();
        fadeOutAnimation.Append(DOTween.To(() => spiritLight.intensity,
            (x) => spiritLight.intensity = x, 1, fadeTime));
        fadeOutAnimation.Join(_spiritRenderer.DOFade(1, fadeTime));
    }

    private void OnValidate()
    {
        if (resetAnimation)
        {
            Debug.Log("Reset Animation");
            transform.position = initialPos;
            transform.localScale = initialScale;
            shakeAnimation.Kill();
            CreateShakeAnimation();
            shakeAnimation.Play();
            resetAnimation = false;
        }
    }

    // Destory on collision with player
    // public void Kill()
    // {
    //     animation.Kill();
    //     MusicController.Instance.PlaySound(MusicController.SoundEffects.Score, .5f);
    //     Destroy(gameObject);
    // }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other is EdgeCollider2D || !other.gameObject.CompareTag("Player")) return;
        shakeAnimation.Kill();
        MusicController.Instance.PlaySound(MusicController.SoundEffects.Score, .5f);
        _itemCollector.WhiteSpiritAmt++;
        Destroy(gameObject);
    }
}