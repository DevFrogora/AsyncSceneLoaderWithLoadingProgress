using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public static class Loader 
{
    private class LoadingMonoBehaviour : MonoBehaviour { }

    public enum Scene{
        LobbyScene,
        IslandScene,
        TrainingScene,
        Loading,
    };

    private static Action onLoaderCallback;
    private static AsyncOperation loadingAsyncOperation;


    public static void Load(Scene scene)
    {
        onLoaderCallback = () =>
        {
            GameObject loadingGameObject = new GameObject("Loading Game Object");
            loadingGameObject.AddComponent<LoadingMonoBehaviour>().StartCoroutine(LoadSceneASync(scene));

        };

        SceneManager.LoadScene(Scene.Loading.ToString());
    }

    private static IEnumerator LoadSceneASync(Scene scene)
    {
        yield return new WaitForSeconds(1); // go one pass frame before loading

        loadingAsyncOperation = SceneManager.LoadSceneAsync(scene.ToString());

        while (!loadingAsyncOperation.isDone)
        {
            yield return null;
        }
    }

    public static float GetLoadingProgress()
    {
        if(loadingAsyncOperation!= null)
        {
            return loadingAsyncOperation.progress;
        }
        else
        {
            return 1f;
        }
    }

    public static void LoaderCallback()
    {
        if(onLoaderCallback!=null)
        {
            onLoaderCallback();
            onLoaderCallback = null;
        }


    }
}
