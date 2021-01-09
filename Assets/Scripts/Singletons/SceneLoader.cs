using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    private static Transform m_DestroyOnLoadGO;

    private void Start()
    {
        MusicController.Instance.PlayMenuBGM();
    }

    public enum Scene
    {
        StartScene,
        GameScene,
    };

    public void MoveToScene(Scene scene)
    {
        MoveToScene(Enum.GetName(typeof(Scene), scene));
    }

    public static void DestroyOnLoad(GameObject aGO)
    {
        if (m_DestroyOnLoadGO == null)
            m_DestroyOnLoadGO = (new GameObject("DestroyOnLoad")).transform;
        aGO.transform.parent = m_DestroyOnLoadGO;
    }

    public void MoveToScene(string sceneName)
    {
        switch (sceneName)
        {
            case "StartScene":
                MusicController.Instance.PlayMenuBGM();
                break;
            case "GameScene":
                MusicController.Instance.PlayGameBGM();
                break;
        }

        DOTween.KillAll();
        SceneManager.LoadScene(sceneName);
    }
    

    public void ExitGame()
    {
        Application.Quit();
    }


    public Scene GetActiveScene()
    {
        var sceneName = SceneManager.GetActiveScene().name;
        return Enum.GetName(typeof(Scene), Scene.GameScene) == sceneName ? Scene.GameScene : Scene.StartScene;
    }
}