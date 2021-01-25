﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelTrigger : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            if (GameManager.Instance.CollectedSpirits == 3)
            {
                UIManager.Instance.EndLevel();
            }
            else
            {
                CluesManager.Instance.PlayClue("Almost There...");
            }
        }
    }
}
