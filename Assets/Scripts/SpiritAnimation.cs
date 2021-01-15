using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpiritAnimation : MonoBehaviour
{
    public float duration = 1f;
    public float strength = 2f;
    private Sequence animation;
    private float intervalTime;
    [Range(1, 5)] private float intervalRandomMin = 1f;
    private float intervalRandomMax = 5f;

    private Vector3 initialPos;
    private Vector3 initialScale;

    [SerializeField] private bool resetAnimation = false;
    [SerializeField] private ItemCollector _itemCollector;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.localPosition;
        initialScale = transform.localScale;
        _itemCollector = FindObjectOfType<ItemCollector>();
        CreateAnimation();
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnEnable()
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
        animation.SetEase(Ease.InOutCubic);
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

    public void KillAnimation()
    {
        animation.Kill();
    }

    public void StartAnimation()
    {
        CreateAnimation();
    }
    // Destory on collision with player
    public void Kill()
    {
        animation.Kill();
        MusicController.Instance.PlaySound(MusicController.SoundEffects.Score, .5f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other is EdgeCollider2D || !other.gameObject.CompareTag("Player")) return;
        animation.Kill();
        _itemCollector.WhiteSpiritAmt++;
        MusicController.Instance.PlaySound(MusicController.SoundEffects.Score,.5f);
        Destroy(gameObject);
    }
}