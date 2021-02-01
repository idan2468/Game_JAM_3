using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FinalPuzzle : MonoBehaviour
{
    [Header("Debugging")] [SerializeField] private SpotsPuzzle _spotsPuzzle;
    [SerializeField] private Sequence _animation;
    [SerializeField] private float timeInStaticCamera;
    [SerializeField] private GameObject _puzzleReturnLocation;

    // Start is called before the first frame update
    void Start()
    {
        _spotsPuzzle = GetComponent<SpotsPuzzle>();
        _spotsPuzzle.EventToTrigger = CrackSpiritRock;
        _spotsPuzzle.EventResetPuzzle = FailPuzzle;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CrackSpiritRock()
    {

    }

    private void FailPuzzle()
    {
        _animation = DOTween.Sequence();
        _animation.AppendCallback(() =>
        {
            GameManager.Instance.FreezePlayer();
            GameManager.Instance.ReturnPlayerToCheckpoint(_puzzleReturnLocation.transform.position);
            GameManager.Instance.ChangeVirtualCamera(GameManager.VirtualCamera.Hook);
        })
            .AppendInterval(timeInStaticCamera + GameManager.Instance.CameraBlendTime)
            .AppendCallback(() => { GameManager.Instance.ChangeVirtualCamera(GameManager.VirtualCamera.River); })
            .AppendInterval(timeInStaticCamera + GameManager.Instance.CameraBlendTime)
            .AppendCallback(() => GameManager.Instance.ChangeVirtualCamera(GameManager.VirtualCamera.Main))
            .AppendInterval(timeInStaticCamera + GameManager.Instance.CameraBlendTime)
            .AppendCallback(() => GameManager.Instance.UnfreezePlayer());
    }
}
