using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CluesManager : Singleton<CluesManager>
{
    private Sequence _animation;
    private Transform _clueTextTransform;
    private float _xLocOfText;
    [Header("Params")] [SerializeField] private float distFromObject;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private TextMeshProUGUI _clueText;
    [SerializeField] private float moveDist;
    [SerializeField] private float fadeInTime;
    [SerializeField] private float fadeOutTime;
    [SerializeField] private float displayTime;
    [SerializeField] private Ease fadeEase = Ease.InOutSine;
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
        _clueTextTransform = _clueText.gameObject.transform;
        _xLocOfText = _clueTextTransform.position.x;
        dictMap = new Dictionary<Transform, string>();
        playAnimation = false;
        // dictMap = new Dictionary<Transform, string>()
        // {
        //     {rockStart,rockLevelText},
        //     {waterStart,waterLevelText},
        //     {windStart,windLevelText},
        // };
        UpdateMapForInteract();
    }

    private void UpdateMapForInteract()
    {
        foreach (Transform interactObject in interactObjects)
        {
            if (interactObject != null)
            {
                dictMap.Add(interactObject, clueInteractText);
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
                PlayClue(keyVal.Value);
            }
        }
    }


    private void OnValidate()
    {
        if (playAnimation)
        {
            PlayClue(clueInteractText);
            Utility.DisableInspectorButton(() => playAnimation = false).Play();
        }
    }

    private void CreateAndPlayAnimation()
    {
        _animation = DOTween.Sequence();
        _animation
            .AppendCallback(() => _clueText.gameObject.SetActive(true))
            .Append(_clueTextTransform.DOMoveX(_xLocOfText - moveDist, 0))
            .Append(_clueText.DOFade(0, 0))
            .Append(_clueTextTransform.DOMoveX(_xLocOfText, fadeInTime)).SetEase(fadeEase)
            .Join(_clueText.DOFade(1, fadeInTime)).SetEase(fadeEase)
            .AppendInterval(displayTime)
            .Append(_clueText.DOFade(0, fadeOutTime)).SetEase(fadeEase)
            .OnComplete(() =>
            {
                var position = _clueTextTransform.position;
                _clueTextTransform.position = new Vector3(_xLocOfText, position.y, position.z);
                _clueText.gameObject.SetActive(false);
            });
    }

    public void PlayClue(string textToShow)
    {
        _animation.Kill(complete: true);
        _clueText.text = textToShow;
        CreateAndPlayAnimation();
    }
}