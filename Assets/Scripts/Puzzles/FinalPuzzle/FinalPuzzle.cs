using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPuzzle : MonoBehaviour
{
    [Header("Debugging")] [SerializeField] private SpotsPuzzle _spotsPuzzle;
    [SerializeField] private GameObject _testObject;

    // Start is called before the first frame update
    void Start()
    {
        _spotsPuzzle = GetComponent<SpotsPuzzle>();
        _spotsPuzzle.EventToTrigger = CrackSpiritRock;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CrackSpiritRock()
    {
        // TODO: CHANGE TO ACTUAL FUNCTION
        _testObject.SetActive(false);
    }
}
