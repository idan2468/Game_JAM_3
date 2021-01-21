using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SpotDownAnimation : MonoBehaviour
{
    [Header("Debugging")] [SerializeField] private SpiritAnimation _spiritAnimation;
    [SerializeField] private Sequence _animation;
    [SerializeField] private SpotsPuzzle _puzzle;
    [Header("Params")] [SerializeField] private GameObject _spirit;
    [SerializeField] private float getDownTime = 1f;

    [SerializeField] private float getDownHeight = 3.5f;

    // Start is called before the first frame update
    void Start()
    {
        _puzzle = GetComponent<SpotsPuzzle>();
        _puzzle.EventToTrigger = GetSpotDownAnimation;
        _spiritAnimation = _spirit.GetComponent<SpiritAnimation>();
        _spirit.SetActive(false);
    }

    private void GetSpotDownAnimation()
    {
        _spiritAnimation.enabled = false;
        _animation = DOTween.Sequence();
        _animation.AppendCallback(() => _spirit.transform.position += Vector3.up * getDownHeight);
        _animation.AppendCallback(() => _spirit.SetActive(true));
        _animation.Append(_spirit.transform.DOMoveY(_spirit.transform.position.y, getDownTime));
        _animation.OnComplete(() =>
        {
            if (_spiritAnimation != null)
            {
                _spiritAnimation.enabled = true;
            }

            _puzzle.TurnOffPuzzle();
        });
        _animation.SetEase(Ease.InOutSine);
        _animation.Play();
    }

    // Update is called once per frame
    void Update()
    {
    }
}