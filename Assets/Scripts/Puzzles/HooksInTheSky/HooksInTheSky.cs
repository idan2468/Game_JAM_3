using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HooksInTheSky : MonoBehaviour
{
    [SerializeField] private Sequence animation;
    [SerializeField] private float fadeTime = 1f;
    [SerializeField] private Ease ease = Ease.InOutSine;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void FadeHook(List<SpriteRenderer> hookSprites, Transform hookToEnable)
    {
        animation = DOTween.Sequence();
        foreach (var sprite in hookSprites)
        {
            animation.Join(sprite.DOFade(0, 0));
        }
        animation.AppendCallback(() =>
        {
            GameManager.Instance.PlayerCanMove = false;
            hookToEnable.gameObject.SetActive(true);
        });
        foreach (var sprite in hookSprites)
        {
            animation.Join(sprite.DOFade(1, fadeTime));
        }

        animation.AppendCallback(() => GameManager.Instance.PlayerCanMove = true);
        animation.SetEase(ease);
    }
}