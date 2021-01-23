using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WaterfallPuzzle : MonoBehaviour
{
    [Header("Debugging")] [SerializeField] private SpotsPuzzle _spotsPuzzle;
    [SerializeField] private GameObject _waterfallRock;
    [SerializeField] private Sequence _fadeInSeq;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _fadeInTime = 2f;
    [SerializeField] private Ease _ease = Ease.InOutSine;


    // Start is called before the first frame update
    void Start()
    {
        //_fadeInSeq = DOTween.Sequence();
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
        _fadeInSeq = DOTween.Sequence();
        _fadeInSeq.AppendCallback(() => _spotsPuzzle.TurnOffPuzzle());
        _fadeInSeq.AppendCallback(() => _waterfallRock.SetActive(true));
        _fadeInSeq.AppendCallback(() =>
        {
            GameManager.Instance.FreezePlayer();
            GameManager.Instance.ChangeVirtualCamera(GameManager.VirtualCamera.Waterfall);
        });
        _fadeInSeq.AppendInterval(GameManager.Instance.CameraBlendTime);
        _fadeInSeq.Append(_spriteRenderer.DOFade(1, _fadeInTime).From(0));
        _fadeInSeq.AppendCallback(() => GameManager.Instance.ChangeVirtualCamera(GameManager.VirtualCamera.Main));
        _fadeInSeq.AppendInterval(GameManager.Instance.CameraBlendTime);
        _fadeInSeq.AppendCallback(() => GameManager.Instance.UnfreezePlayer());
        _fadeInSeq.SetEase(_ease);
    }
}