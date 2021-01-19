using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


[Serializable]
public class Clue
{
    [SerializeField] private Transform objPos;
    [SerializeField] private String clueText;

    public String ClueText
    {
        get => clueText;
        set { clueText = value; }
    }

    public Transform ObjPos
    {
        get => objPos;
        set { objPos = value; }
    }

    public Clue(Transform objPos, String clueText)
    {
        this.objPos = objPos;
        this.clueText = clueText;
    }
}


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
    [SerializeField] private float delayForShowingAgainClue = 5f;

    [Header("Clues")] [SerializeField] private List<Clue> cluesRock;
    [SerializeField] private List<Clue> cluesWater;
    [SerializeField] private List<Clue> cluesAir;
    [Header("Debugging")] [SerializeField] private List<Clue> allClues;
    Dictionary<Transform, String> dictMap;
    [SerializeField] private List<Clue> notToShowClues;


    // Start is called before the first frame update
    void Start()
    {
        _clueText = GameObject.FindWithTag("ClueText").GetComponent<TextMeshProUGUI>();
        _clueText.gameObject.SetActive(false);
        _clueTextTransform = _clueText.gameObject.transform;
        _xLocOfText = _clueTextTransform.position.x;
        playAnimation = false;
        MergeAllCluesList();
    }

    private void MergeAllCluesList()
    {
        allClues = new List<Clue>();
        if (cluesRock != null)
        {
            allClues.AddRange(cluesRock);
        }

        if (cluesWater != null)
        {
            allClues.AddRange(cluesWater);
        }

        if (cluesAir != null)
        {
            allClues.AddRange(cluesAir);
        }

        allClues.RemoveAll((clue) => clue.ObjPos == null);
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
        foreach (var clue in allClues)
        {
            if (!notToShowClues.Contains(clue))
            {
                var objPos = clue.ObjPos.position;
                var clueText = clue.ClueText;
                if (Vector3.Distance(objPos, playerTransform.position) <= distFromObject)
                {
                    PlayClue(clueText);
                    StartCoroutine(DelayForNextShow(clue));
                }
            }
        }
    }

    private IEnumerator DelayForNextShow(Clue clue)
    {
        notToShowClues.Add(clue);
        yield return new WaitForSeconds(delayForShowingAgainClue);
        notToShowClues.Remove(clue);
    }


    private void OnValidate()
    {
        if (playAnimation)
        {
            if (allClues.Count > 0)
            {
                PlayClue(allClues[0].ClueText);
            }
            else
            {
                Debug.LogWarning("No Clues");
            }

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