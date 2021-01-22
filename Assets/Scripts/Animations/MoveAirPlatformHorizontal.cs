using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MoveAirPlatformHorizontal : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private float dist = 5f;
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private Ease ease = Ease.InOutSine;

    private Tween _animation;
    // Start is called before the first frame update
    void Start()
    {
        _animation = transform.DOMoveX(transform.position.x - dist, dist / speed)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(ease);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
