using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FinalPuzzle : MonoBehaviour
{
    [Header("Debugging")] [SerializeField] private SpotsPuzzle _spotsPuzzle;
    [SerializeField] private Sequence _animation;
    [SerializeField] private float timeInStaticCamera;
    [SerializeField] private CheckpointEnterEvent _puzzleCheckpointScript;
    [SerializeField] private GameObject _finalSpirit;
    [SerializeField] private GameObject _tempAscent;

    // Start is called before the first frame update
    void Start()
    {
        _spotsPuzzle = GetComponent<SpotsPuzzle>();
        _spotsPuzzle.EventToTrigger = CompletePuzzle;
        _spotsPuzzle.EventResetPuzzle = FailPuzzle;
        _puzzleCheckpointScript.EventToTrigger = TriggerZoomEvent;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CompletePuzzle()
    {
        _animation = DOTween.Sequence()
            .AppendCallback(() =>
            {
                GameManager.Instance.FreezePlayer();
                GameManager.Instance.ChangeVirtualCamera(GameManager.VirtualCamera.HooksInTheSky);
            })
            .AppendInterval(timeInStaticCamera + GameManager.Instance.CameraBlendTime)
            .AppendCallback(() =>
            {
                _finalSpirit.SetActive(true);
                _tempAscent.SetActive(true);
                _finalSpirit.GetComponent<SpiritAnimation>().TriggerFadeInAnimation();
            })
            .AppendCallback(() => _spotsPuzzle.TurnOffPuzzle())
            .AppendCallback(() => GameManager.Instance.ChangeVirtualCamera(GameManager.VirtualCamera.Main))
            .AppendInterval(GameManager.Instance.CameraBlendTime)
            .AppendCallback(() => GameManager.Instance.UnfreezePlayer());
    }

    private void FailPuzzle()
    {
        _animation = DOTween.Sequence();
        _animation.AppendCallback(() =>
        {
            GameManager.Instance.FreezePlayer();
            GameManager.Instance.ReturnPlayerToCheckpoint(_puzzleCheckpointScript.transform.position);
        });
    }

    public void TriggerZoomEvent()
    {
        _animation = DOTween.Sequence()
            .AppendCallback(() => {
                GameManager.Instance.FreezePlayer();
                GameManager.Instance.ChangeVirtualCamera(GameManager.VirtualCamera.SequenceElements); 
            })
            .AppendInterval(timeInStaticCamera + GameManager.Instance.CameraBlendTime)
            .AppendCallback(() => GameManager.Instance.ChangeVirtualCamera(GameManager.VirtualCamera.Main))
            .AppendInterval(timeInStaticCamera + GameManager.Instance.CameraBlendTime)
            .AppendCallback(() => GameManager.Instance.UnfreezePlayer());
    }
}
