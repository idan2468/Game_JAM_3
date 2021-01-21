using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _endMenu;
    [SerializeField] private GameObject _continueMenu;
    [SerializeField] private GameObject menuCanvas;
    [SerializeField] private Image background;
    [SerializeField] private float backgroundOpacity;
    [SerializeField] private float backgroundFadeTime = 1f;
    [SerializeField] private Ease fadeEase = Ease.InOutSine;


    [SerializeField] private TextMeshProUGUI _whiteSpiritAmount;
    [SerializeField] private GameObject gameScreenCanvas;

    [SerializeField] private GameObject playerGO;
    private ItemCollector _playerItemCollector;

    public enum GameScene
    {
        Rock,
        Water,
        Wind
    }

    // Start is called before the first frame update
    void Start()
    {
        _playerItemCollector = playerGO.GetComponent<ItemCollector>();
        _whiteSpiritAmount = GameObject.FindWithTag("SpiritAmt").GetComponent<TextMeshProUGUI>();
        background = GameObject.FindWithTag("Background").GetComponent<Image>();
        backgroundOpacity = background.color.a;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore();

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

    private void UpdateScore()
    {
        _whiteSpiritAmount.text = _playerItemCollector.SpiritsAmt.ToString();
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