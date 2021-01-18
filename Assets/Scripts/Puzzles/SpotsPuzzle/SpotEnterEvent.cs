using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotEnterEvent : MonoBehaviour
{
    // Start is called before the first frame update
    private SpotsPuzzle _puzzle;
    void Start()
    {
        _puzzle = gameObject.transform.parent.GetComponentInParent<SpotsPuzzle>();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: CHECK DIMMING
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Enter");
        _puzzle.SpotActivated(gameObject,other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Exit");
        _puzzle.SpotDeactivated(gameObject,other.gameObject);
    }
}