using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SpotEnterEvent : MonoBehaviour
{
    // Start is called before the first frame update
    private SpotsPuzzle _puzzle;
    private SpotLightEffect _lightEffect;
    [SerializeField] private bool fadeOutLightsOnEnter = true;

    void Start()
    {
        _puzzle = gameObject.transform.parent.GetComponentInParent<SpotsPuzzle>();
        _lightEffect = GetComponent<SpotLightEffect>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Enter");
        _puzzle.SpotActivated(gameObject, other.gameObject);
        if (fadeOutLightsOnEnter)
            _lightEffect.FadeOutLight();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Exit");
        if (!_puzzle.IsBySequence)
        {
            _puzzle.SpotDeactivated(gameObject, other.gameObject);
        }
        if (fadeOutLightsOnEnter)
            _lightEffect.FadeInLight();
    }
}