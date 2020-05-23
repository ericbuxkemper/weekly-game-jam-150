using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderUI : MonoBehaviour
{
    private Coroutine _loadSceneRoutine = null;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void LoadScene(string sceneName)
    {
        if (_loadSceneRoutine == null)
            _loadSceneRoutine = StartCoroutine(WaitForSceneToLoad(sceneName));
    }

    private IEnumerator WaitForSceneToLoad(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (true)
        {
            if (asyncLoad.isDone)
            {
                // start loading screen animation
                Debug.Log("Loading Scene" + sceneName);
                _loadSceneRoutine = null;
                break;
            }

            yield return null;
        }
    }
}
