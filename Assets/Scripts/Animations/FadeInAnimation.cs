using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FadeInAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private Image background;
    [SerializeField]private float fadeTime = 1f;
    [SerializeField] private Ease ease = Ease.InOutSine;
    void Start()
    {
        background = GetComponent<Image>();
        // background = GameObject.FindWithTag("Background").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        background.DOFade(1, 1f).SetEase(ease).From(0);
    }
}
