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
    [SerializeField] private GameObject _checkpoint;
    [SerializeField] private List<Transform> _resetObjects;
    [SerializeField] private List<Vector3> _resetCoordinates;
    private CheckpointEnterEvent _checkpointScript;

    // Start is called before the first frame update
    void Start()
    {
        _spotsPuzzle = GetComponent<SpotsPuzzle>();
        _spotsPuzzle.EventToTrigger = MoveRockAnimation;
        _checkpointScript = _checkpoint.GetComponent<CheckpointEnterEvent>();
        _checkpointScript.EventToTrigger = ResetPuzzle;

        // Save coordinates of stones
        var objectsToReset = transform.Find("MoveableRocks");
        foreach(Transform childTransform in objectsToReset)
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

    private void ResetPuzzle()
    {
        for (int i = 0; i < _resetObjects.Count; ++i)
        {
            _resetObjects[i].position = _resetCoordinates[i];
        }
    }
    public void StopReset()
    {
        _checkpointScript.Reset = false;
    }
}