using System;
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
    //private UIManager _uiManger;
    private float intervalTime;
    [Range(1,5)]
    private float intervalRandomMin = 1f;
    private float intervalRandomMax = 5f;
    // Start is called before the first frame update
    void Start()
    {
        //_uiManger = FindObjectOfType<UIManager>();
        animation = DOTween.Sequence();
        animation.Append(gameObject.transform.DOShakeScale(duration, strength));
        animation.Join(gameObject.transform.DOShakePosition(duration, strength));
        animation.AppendCallback(() => intervalTime = Random.Range(intervalRandomMin, intervalRandomMax));
        animation.AppendInterval(intervalTime);
        animation.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other is EdgeCollider2D) return;
        animation.Kill();
        //_uiManger.AddScore();
        Destroy(gameObject);
    }
}
