using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HookPuzzle : MonoBehaviour
{
    [Header("Params")] [SerializeField] private MoveAirPlatformHorizontal movingRock;
    [SerializeField] private float timeInHookCamera;
    [SerializeField] private CheckpointEnterEvent _checkpointScript;
    [SerializeField] private Ease ease = Ease.InOutSine;
    [SerializeField] private Transform objectsToResetContainer;
    [Header("Debugging")] [SerializeField] private SpotsPuzzle _spotsPuzzle;
    [SerializeField] private Sequence _animation;
    [SerializeField] private List<Transform> _resetObjects;
    [SerializeField] private List<Vector3> _resetCoordinates;

    // Start is called before the first frame update
    void Start()
    {
        _spotsPuzzle = GetComponent<SpotsPuzzle>();
        _spotsPuzzle.EventToTrigger = MoveRockAnimation;
        _checkpointScript.EventToTrigger = ResetPuzzle;

        foreach (Transform childTransform in objectsToResetContainer)
        {
            _resetObjects.Add(childTransform);
            _resetCoordinates.Add(childTransform.position);
        }
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
            GameManager.Instance.FreezePlayer();
        });
        _animation.AppendInterval(GameManager.Instance.CameraBlendTime);
        _animation.AppendCallback(() => movingRock.enabled = true);
        _animation.AppendInterval(timeInHookCamera + GameManager.Instance.CameraBlendTime);
        _animation.AppendCallback(() => GameManager.Instance.ChangeVirtualCamera(GameManager.VirtualCamera.Main));
        _animation.AppendInterval(GameManager.Instance.CameraBlendTime);
        _animation.AppendCallback(() => GameManager.Instance.UnfreezePlayer());
        _animation.SetEase(ease);
        StopReset();
    }

    private void ResetPuzzle()
    {
        for (int i = 0; i < _resetObjects.Count; ++i)
        {
            _resetObjects[i].position = _resetCoordinates[i];
        }

        _animation = DOTween.Sequence();
        _animation.AppendCallback(() =>
            {
                GameManager.Instance.FreezePlayer();
                GameManager.Instance.ChangeVirtualCamera(GameManager.VirtualCamera.Hook);
            })
            .AppendInterval(GameManager.Instance.CameraBlendTime + timeInHookCamera)
            .AppendCallback(() => GameManager.Instance.ChangeVirtualCamera(GameManager.VirtualCamera.Main))
            .AppendInterval(GameManager.Instance.CameraBlendTime)
            .AppendCallback(() => GameManager.Instance.UnfreezePlayer());
    }

    private void StopReset()
    {
        _checkpointScript.TriggerCheckpointEvent = false;
    }
}