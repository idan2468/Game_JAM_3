using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WaterfallPuzzle : MonoBehaviour
{
    [Header("Debugging")] 
    [SerializeField] private SpotsPuzzle _spotsPuzzle;
    [SerializeField] private GameObject _waterfallRock;
    [SerializeField] private Tween _animation;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _fadeInTime = 2f;


    // Start is called before the first frame update
    void Start()
    {
        _spotsPuzzle = GetComponent<SpotsPuzzle>();
        _spriteRenderer = _waterfallRock.GetComponent<SpriteRenderer>();
        _spotsPuzzle.EventToTrigger = DropWaterfallRock;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DropWaterfallRock()
    {
        _waterfallRock.SetActive(true);
        _animation = _spriteRenderer.DOFade(1, _fadeInTime).From(0);
    }
}
