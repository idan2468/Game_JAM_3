using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RoadToNowherePuzzle : MonoBehaviour
{
    // Start is called before the first frame update
    private SpotsPuzzle _puzzle;
    [Header("Params")] [SerializeField] private SpriteRenderer rockBlockingGeyser;
    [SerializeField] private Collider2D geyserCollider;
    [SerializeField] private float fadeTime = 1f;
    [SerializeField] private Ease ease;
    [SerializeField] private float delayWatchingGeyserRelease = 1f;
    [Header("Debugging")] [SerializeField] private Sequence _animation;

    void Start()
    {
        _puzzle = GetComponent<SpotsPuzzle>();
        _puzzle.EventToTrigger = RemoveRockAnimation;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void RemoveRockAnimation()
    {
        _animation = DOTween.Sequence();
        _animation.AppendCallback(() =>
        {
            GameManager.Instance.ChangeVirtualCamera(GameManager.VirtualCamera.RoadToNowhere);
            GameManager.Instance.FreezePlayer();
        });
        _animation.AppendInterval(GameManager.Instance.CameraBlendTime);
        _animation.Append(
            rockBlockingGeyser.DOFade(0, fadeTime)
            .SetEase(ease)
            .OnComplete(() => rockBlockingGeyser.gameObject.SetActive(false))
            );
        _animation.AppendInterval(delayWatchingGeyserRelease);
        _animation.AppendCallback(() => GameManager.Instance.ChangeVirtualCamera(GameManager.VirtualCamera.Main));
        _animation.AppendInterval(GameManager.Instance.CameraBlendTime);
        _animation.AppendCallback(() =>
        {
            geyserCollider.enabled = true;
            GameManager.Instance.UnfreezePlayer();
            _puzzle.TurnOffPuzzle();
        });
        _animation.SetEase(ease);
    }
}