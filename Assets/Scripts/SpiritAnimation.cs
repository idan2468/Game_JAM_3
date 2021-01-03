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
    private UIManger _uiManger;
    // Start is called before the first frame update
    void Start()
    {
        _uiManger = FindObjectOfType<UIManger>();
        animation = DOTween.Sequence();
        animation.Append(gameObject.transform.DOShakePosition(duration, strength));
        animation.AppendInterval(1f);
        animation.SetLoops(-1);
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
        _uiManger.AddScore();
        Destroy(gameObject);
    }
}
