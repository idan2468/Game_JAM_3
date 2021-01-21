using System.Collections;
using System.Collections.Generic;
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
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore();

        // Pause Menu
        if (Input.GetKey(KeyCode.Escape) && !menuCanvas.activeSelf)
        {
            GameManager.Instance.StopTime();
            _pauseMenu.SetActive(true);
            menuCanvas.SetActive(true);
        }
    }

    private void UpdateScore()
    {
        _whiteSpiritAmount.text = _playerItemCollector.SpiritsAmt.ToString();
    }

    private void ContinueGame()
    {
        GameManager.Instance.StopTime();
        menuCanvas.SetActive(true);
        _continueMenu.SetActive(true);
    }

    private void EndGame()
    {
        GameManager.Instance.StopTime();
        menuCanvas.SetActive(true);
        _endMenu.SetActive(true);
    }

    public void EndLevel()
    {
        switch(SceneManager.GetActiveScene().buildIndex)
        {
            case (int)GameScene.Rock:
                ContinueGame();
                break;
            case (int)GameScene.Water:
                EndGame();
                break;
            case (int)GameScene.Wind:
                EndGame();
                break;
        }
    }
}
