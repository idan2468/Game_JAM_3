using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HookPuzzle : MonoBehaviour
{
    [Header("Params")] [SerializeField] private MoveAirPlatformHorizontal movingRock;
    [SerializeField] private float timeInHookCamera;

    [SerializeField] private Ease ease = Ease.InOutSine;
    [Header("Debugging")] [SerializeField] private SpotsPuzzle _spotsPuzzle;
    [SerializeField] private Sequence _animation;


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
        _animation = DOTween.Sequence();
        _animation.AppendCallback(() =>
        {
            GameManager.Instance.ChangeVirtualCamera(GameManager.VirtualCamera.Hook);
            GameManager.Instance.PlayerCanMove = false;
        });
        _animation.AppendInterval(GameManager.Instance.CameraBlendTime);
        _animation.AppendCallback(() => movingRock.enabled = true);
        _animation.AppendInterval(timeInHookCamera);
        _animation.AppendCallback(() =>
            {
                GameManager.Instance.ChangeVirtualCamera(GameManager.VirtualCamera.Main);
                GameManager.Instance.PlayerCanMove = true;
            })
            .SetEase(ease);
    }
}