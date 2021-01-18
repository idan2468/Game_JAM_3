using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _endMenu;
    [SerializeField] private GameObject menuCanvas;

    [SerializeField] private TextMeshProUGUI _whiteSpiritAmount;
    [SerializeField] private GameObject gameScreenCanvas;

    [SerializeField] private GameObject playerGO;
    private ItemCollector _playerItemCollector;

    // Start is called before the first frame update
    void Start()
    {
        _playerItemCollector = playerGO.GetComponent<ItemCollector>();
        _whiteSpiritAmount = gameScreenCanvas.transform.Find("WhiteSpiritIcon").transform.Find("WhiteAmount").gameObject.GetComponent<TextMeshProUGUI>();
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

    public void EndLevel()
    {
        // Endgame Menu
        if (_playerItemCollector.SpiritsAmt == 3)
        {
            GameManager.Instance.StopTime();
            menuCanvas.SetActive(true);
            _endMenu.SetActive(true);
        }
    }
}
