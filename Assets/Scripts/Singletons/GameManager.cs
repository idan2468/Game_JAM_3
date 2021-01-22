using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    // TODO: LOOK INTO SAVING THE GAME
    [SerializeField] private GameObject _player;

    [Header("Killing Animation")] [SerializeField]
    private bool vanishPlayerOnMovement = false;
    [SerializeField] private float movingToCheckpointSpeed = 10f;

    [SerializeField] private Ease ease = Ease.InOutSine;
    private PlayerMove _playerMoveScript;
    private PlayerMove _playerItemScript;
    private Animator _animator;
    
    public enum VirtualCamera
    {
        Main,
        Waterfall,
        Hook,
        River
    }


    private void Start()
    {
        _playerMoveScript = _player.GetComponent<PlayerMove>();
        _playerMoveScript.CanMove = false; // Freeze player but not time
        MusicController.Instance.PlayMenuBGM();
        _animator = GetComponent<Animator>();
        StartTime();
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
        ResetSingletons();
        SceneManager.LoadScene(0);
    }

    public void ResetSingletons()
    {
        DOTween.KillAll();
        Destroy(UIManager.Instance.gameObject);
        Destroy(GameManager.Instance.gameObject);
        Destroy(MusicController.Instance.gameObject);
        Destroy(CluesManager.Instance.gameObject);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ReturnPlayerToCheckpoint(Vector3 checkPoint)
    {
        var dist = Vector3.Distance(_player.transform.position, checkPoint);
        DOTween.Sequence()
            .AppendCallback(() =>
            {
                _playerMoveScript.CanMove = false;
                if (vanishPlayerOnMovement)
                {
                    _player.SetActive(false);
                }
            })
            .Append(_player.transform.DOMove(checkPoint,
                movingToCheckpointSpeed != 0 ? dist / movingToCheckpointSpeed : movingToCheckpointSpeed))
            .AppendCallback(() =>
            {
                _playerMoveScript.CanMove = true;
                if (vanishPlayerOnMovement)
                {
                    _player.SetActive(true);
                }
            }).SetEase(ease).Play();
    }

    public void AdvanceLevel()
    {
        ResetSingletons();
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        if (currentScene < (int)UIManager.GameScene.Wind)
        {
            SceneManager.LoadScene(currentScene + 1);
        }
    }

    public void ChangeVirtualCamera(VirtualCamera virtualCamera)
    {
        _animator.SetTrigger(Enum.GetName(typeof(VirtualCamera),virtualCamera));
    }
}