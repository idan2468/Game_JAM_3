using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject menuBackground;

    [SerializeField] private TextMeshProUGUI _whiteSpiritAmount;
    [SerializeField] private TextMeshProUGUI _blueSpiritAmount;

    [SerializeField] private Transform junk;
    public GameObject gameScreenCanvas;

    public GameObject playerGO;
    private ItemCollector _playerItemCollector;

    // Start is called before the first frame update
    void Start()
    {
        _playerItemCollector = playerGO.GetComponent<ItemCollector>();
        _whiteSpiritAmount = gameScreenCanvas.transform.Find("WhiteSpiritIcon").transform.Find("WhiteAmount").gameObject.GetComponent<TextMeshProUGUI>();
        _blueSpiritAmount = gameScreenCanvas.transform.Find("BlueSpiritIcon").transform.Find("BlueAmount").gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore();

        // TODO: UNCOMMENT
        //if (Input.GetKey(KeyCode.Escape))
        //{
        //    GameManager.Instance.StopTime();
        //    _pauseMenu.SetActive(true);
        //    menuBackground.SetActive(true);
        //}
    }

    private void UpdateScore()
    {
        _whiteSpiritAmount.text = _playerItemCollector.WhiteSpiritAmt.ToString();
        _blueSpiritAmount.text = _playerItemCollector.BlueSpiritAmt.ToString();
    }
}
