using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HookPuzzle : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private Transform thirdHook;

    [SerializeField] private float dist = 3f;

    [SerializeField] private float speed = 3f;

    [SerializeField] private Tween _animation;

    [SerializeField] private Ease ease = Ease.InOutSine;
    [Header("Debugging")] [SerializeField] private SpotsPuzzle _spotsPuzzle;

    // Start is called before the first frame update
    void Start()
    {
        _spotsPuzzle = GetComponent<SpotsPuzzle>();
        _spotsPuzzle.EventToTrigger = MoveRockAnimation;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void MoveRockAnimation()
    {
        _animation = thirdHook.DOMoveX(thirdHook.position.x - dist, dist / speed)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(ease);
    }
}