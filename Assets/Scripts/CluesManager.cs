using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CluesManager : MonoBehaviour
{
    private Sequence animation;
    private Transform clueTextTransform;
    private float xLocOfText;
    [Header("Params")] [SerializeField] private float distFromObject;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private TextMeshProUGUI _clueText;
    [SerializeField] private float moveDist;
    [SerializeField] private float fadeInTime;
    [SerializeField] private float fadeOutTime;
    [SerializeField] private float displayTime;
    [SerializeField] private bool restartAnimation;
    [SerializeField] private Ease fadeEase = Ease.InOutSine;
    [SerializeField] private bool animationNotPlaying;
    [SerializeField] private bool playAnimation;

    [Header("Use Interact")] [SerializeField]
    private Transform[] interactObjects;
    [SerializeField] private String clueInteractText;

    [Header("Rock Level Start")] [SerializeField]
    private Transform rockStart;

    [SerializeField] private String rockLevelText;

    [Header("Water Level Start")] [SerializeField]
    private Transform waterStart;

    [SerializeField] private String waterLevelText;

    [Header("Wind Level Start")] [SerializeField]
    private Transform windStart;

    [SerializeField] private String windLevelText;

    Dictionary<Transform, String> dictMap;



    // Start is called before the first frame update
    void Start()
    {
        restartAnimation = false;
        clueTextTransform = _clueText.gameObject.transform;
        xLocOfText = clueTextTransform.position.x;
        dictMap = new Dictionary<Transform, string>();
        // dictMap = new Dictionary<Transform, string>()
        // {
        //     {rockStart,rockLevelText},
        //     {waterStart,waterLevelText},
        //     {windStart,windLevelText},
        // };
        UpdateMapForInteract();
        CreateAnimation();
        animationNotPlaying = true;
    }

    private void UpdateMapForInteract()
    {
        foreach (Transform interactObject in interactObjects)
        {
            if (interactObject != null)
            {
                dictMap.Add(interactObject,clueInteractText);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!_clueText.gameObject.activeSelf)
        {
            SearchForClueTrigger();    
        }
    }


    private void SearchForClueTrigger()
    {
        foreach (var keyVal in dictMap)
        {
            if (Vector3.Distance(keyVal.Key.position, playerTransform.position) <= distFromObject)
            {
                CreateAnimation();
                _clueText.text = keyVal.Value;
                animation.Play();
            }
        }
    }
    
    
    private void OnValidate()
    {
        if (restartAnimation)
        {
            animation.Kill();
            CreateAnimation();
            restartAnimation = false;
        }

        if (playAnimation)
        {
            if (!animation.IsPlaying())
            {
                CreateAnimation();
                _clueText.text = clueInteractText;
                animation.Play();
            }
            else
            {
                Debug.LogWarning("Animation is playing");
            }

            playAnimation = false;
        }
    }

    private void CreateAnimation()
    {
        animation = DOTween.Sequence();
        animation
            .AppendCallback(() => _clueText.gameObject.SetActive(true))
            .Append(clueTextTransform.DOMoveX(xLocOfText - moveDist, 0))
            .Append(_clueText.DOFade(0, 0))
            .Append(clueTextTransform.DOMoveX(xLocOfText, fadeInTime)).SetEase(fadeEase)
            .Join(_clueText.DOFade(1, fadeInTime)).SetEase(fadeEase)
            .AppendInterval(displayTime)
            .Append(_clueText.DOFade(0, fadeOutTime)).SetEase(fadeEase)
            .OnComplete(() =>
            {
                var position = clueTextTransform.position;
                clueTextTransform.position = new Vector3(xLocOfText, position.y, position.z);
                _clueText.gameObject.SetActive(false);
            });
    }
}