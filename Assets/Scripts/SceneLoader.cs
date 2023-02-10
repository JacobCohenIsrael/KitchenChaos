using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    private static Scene activeScene;
    
    public enum Scene
    {
        MainMenuScene,
        LoadingScene,
        GameScene
    }

    public static void Load(Scene targetScene)
    {
        activeScene = targetScene;
        // CR: easier way is using loading scene async
        SceneManager.LoadSceneAsync(Scene.LoadingScene.ToString()).completed += OnLoadingSceneLoaded;
    }

    private static void OnLoadingSceneLoaded(AsyncOperation obj)
    {
        SceneManager.LoadScene(activeScene.ToString());

    }
}
