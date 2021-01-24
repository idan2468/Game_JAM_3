using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    [Header("General refs")] [SerializeField]
    private GameObject _pauseMenu;

    [SerializeField] private GameObject _endMenu;
    [SerializeField] private GameObject _continueMenu;
    [SerializeField] private GameObject menuCanvas;
    [SerializeField] private GameObject playerGO;

    [Header(("Background fade params"))] [SerializeField]
    private Image background;

    [SerializeField] private float backgroundOpacity;
    [SerializeField] private float backgroundFadeTime = 1f;
    [SerializeField] private Ease fadeEase = Ease.InOutSine;

    
    private Sequence animation;
    [Header("UI Spirits")]
    [SerializeField] private float fadeTime;
    [SerializeField] private Ease UISpiritEase = Ease.InOutSine;
    [SerializeField] private List<Sprite> emptySpirits;
    [SerializeField] private List<Sprite> fullSpirits;
    [SerializeField] private Sprite currEmptySpirit;
    [SerializeField] private Sprite currFullSpirit;
    [SerializeField] private Transform uiImagesObjectsContainer;
    [SerializeField] private List<Image> _spiritsImages;


    public enum GameScene
    {
        Rock,
        Water,
        Wind
    }

    // Start is called before the first frame update
    void Start()
    {
        // _whiteSpiritAmount = GameObject.FindWithTag("SpiritAmt").GetComponent<TextMeshProUGUI>();
        background = GameObject.FindWithTag("Background")?.GetComponent<Image>();
        if (background != null)
            backgroundOpacity = background.color.a;
        SetSpiritsImages();
    }

    private void SetSpiritsImages()
    {
        uiImagesObjectsContainer = GameObject.FindWithTag("UISpirits").transform;
        _spiritsImages = new List<Image>();
        var activeScene = SceneManager.GetActiveScene().buildIndex;
        currEmptySpirit = emptySpirits[activeScene];
        currFullSpirit = fullSpirits[activeScene];
        foreach (RectTransform imageGO in uiImagesObjectsContainer)
        {
            var image = imageGO.gameObject.GetComponent<Image>();
            image.sprite = currEmptySpirit;
            _spiritsImages.Add(image);
        }
        _spiritsImages.Reverse();
    }

    // Update is called once per frame
    void Update()
    {
        // UpdateScore();

        // Pause Menu
        if (Input.GetKey(KeyCode.Escape) && !menuCanvas.activeSelf)
        {
            EnterPauseMenu();
        }
    }

    private void EnterPauseMenu()
    {
        _pauseMenu.SetActive(true);
        menuCanvas.SetActive(true);
        FadeInBackground(() => GameManager.Instance.StopTime());
    }

    public void ExitPauseMenu()
    {
        GameManager.Instance.StartTime();
        FadeOutBackground(() =>
        {
            _pauseMenu.SetActive(false);
            menuCanvas.SetActive(false);
        });
    }

    public void ActiveUISpirit()
    {
        Debug.Log("ActiveUISpirit");
        animation = DOTween.Sequence();
        animation.Append(_spiritsImages[0].DOFade(0, fadeTime).From(1));
        animation.AppendCallback(() => _spiritsImages[0].sprite = currFullSpirit);
        animation.Append(_spiritsImages[0].DOFade(1, fadeTime).From(0));
        animation.SetEase(UISpiritEase);
        animation.OnComplete(() => _spiritsImages.Remove(_spiritsImages[0]));
    }

    private void ContinueToNextLevel()
    {
        menuCanvas.SetActive(true);
        _continueMenu.SetActive(true);
        FadeInBackground(() => GameManager.Instance.StopTime());
    }

    private void FadeInBackground(TweenCallback callback)
    {
        background.DOFade(backgroundOpacity, backgroundFadeTime).From(0).SetEase(fadeEase).OnComplete(callback);
    }

    private void FadeOutBackground(TweenCallback callback)
    {
        background.DOFade(0, backgroundFadeTime).From(backgroundOpacity).SetEase(fadeEase).OnComplete(callback);
    }

    private void EndGame()
    {
        menuCanvas.SetActive(true);
        _endMenu.SetActive(true);
        FadeInBackground(() => GameManager.Instance.StopTime());
    }

    public void EndLevel()
    {
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case (int) GameScene.Rock:
                ContinueToNextLevel();
                break;
            case (int) GameScene.Water:
                EndGame();
                break;
            case (int) GameScene.Wind:
                EndGame();
                break;
        }
    }
}