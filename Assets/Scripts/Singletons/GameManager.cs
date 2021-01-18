using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    // TODO: LOOK INTO SAVING THE GAME
    [SerializeField] private GameObject _player;

    [Header("Killing Animation")] [SerializeField]
    private float movingToCheckpointSpeed = 10f;

    [SerializeField] private Ease ease = Ease.InOutSine;
    private PlayerMove _playerMoveScript;
    private PlayerMove _playerItemScript;


    private void Start()
    {
        _playerMoveScript = _player.GetComponent<PlayerMove>();
        _playerMoveScript.CanMove = false; // Freeze player but not time
        MusicController.Instance.PlayMenuBGM();
    }

    // TODO: CHECK IF MOVE THIS TO DIFFERENT SCRIPT
    private void Update()
    {
    }

    public void StopTime()
    {
        Time.timeScale = 0;
        _playerMoveScript.CanMove = false;
    }

    // TODO: CHECK IF MOVE THIS TO DIFFERENT SCRIPT
    public void StartTime()
    {
        Time.timeScale = 1;
        _playerMoveScript.CanMove = true;
    }

    public void ResetScene()
    {
        DOTween.KillAll();
        Destroy(UIManager.Instance.gameObject);
        Destroy(GameManager.Instance.gameObject);
        Destroy(MusicController.Instance.gameObject);
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ReturnPlayerToCheckpoint(Vector3 checkPoint)
    {
        var dist = Vector3.Distance(_player.transform.position, checkPoint);
        DOTween.Sequence()
            .AppendCallback(() => _playerMoveScript.CanMove = false)
            .Append(_player.transform.DOMove(checkPoint,
                movingToCheckpointSpeed != 0 ? dist / movingToCheckpointSpeed : movingToCheckpointSpeed))
            .AppendCallback(() => _playerMoveScript.CanMove = true).SetEase(ease).Play();
    }
}