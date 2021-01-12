using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelTrigger : MonoBehaviour
{
    public GameObject playerGO;
    private ItemCollector _playerItemCollector;

    // Start is called before the first frame update
    void Start()
    {
        _playerItemCollector = playerGO.GetComponent<ItemCollector>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            // Endgame Menu
            if (_playerItemCollector.WhiteSpiritAmt == 3)
            {
                UIManager.Instance.EndLevel();
            }
        }
    }
}
