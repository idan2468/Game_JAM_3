using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _endMenu;
    [SerializeField] private GameObject menuBackground;

    [SerializeField] private TextMeshProUGUI _whiteSpiritAmount;
    public GameObject gameScreenCanvas;

    public GameObject playerGO;
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

        // Endgame Menu
        if (_playerItemCollector.WhiteSpiritAmt == 3)
        {
            GameManager.Instance.StopTime();
            _endMenu.SetActive(true);
            menuBackground.SetActive(true);
        }

        // Pause Menu
        if (Input.GetKey(KeyCode.Escape))
        {
            GameManager.Instance.StopTime();
            _pauseMenu.SetActive(true);
            menuBackground.SetActive(true);
        }
    }

    private void UpdateScore()
    {
        _whiteSpiritAmount.text = _playerItemCollector.WhiteSpiritAmt.ToString();
    }
}
