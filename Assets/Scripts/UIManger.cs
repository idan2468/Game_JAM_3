using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManger : MonoBehaviour
{
    public TextMeshProUGUI _score;

    private int currScore = 0;
    // Start is called before the first frame update
    void Start()
    {
        _score.text = "Score " + currScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore()
    {
        currScore++;
        _score.text = "Score " + currScore;
    }
}
