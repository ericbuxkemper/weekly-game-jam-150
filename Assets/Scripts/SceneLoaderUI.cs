using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoaderUI : MonoBehaviour
{
    private Coroutine _loadSceneRoutine = null;
    private Coroutine _showLoadingScreenRoutine = null;
    private Coroutine _hideLoadingScreenRoutine = null;
    private bool shouldHideLoadingScreen = false;
    [SerializeField] private Image _loadingPane = null;
    private AsyncOperation _asyncLoad;

    private float _loadingPaneWidth;
    
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void LoadScene(string sceneName)
    {
        if (_loadSceneRoutine == null)
            _loadSceneRoutine = StartCoroutine(WaitForSceneToLoad(sceneName));
    }

    public void TestButton()
    {
        NarrativeText.Instance.ShowText("TESTING THE NARRATIVE TEXT BOX............");
    }

    private IEnumerator WaitForSceneToLoad(string sceneName)
    {
        _asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        _asyncLoad.allowSceneActivation = false;
        if (_showLoadingScreenRoutine == null)
            _showLoadingScreenRoutine = StartCoroutine(ShowLoadingScreen());
        while (true)
        {
            yield return null;
            Debug.Log(_asyncLoad.progress);
            if (_asyncLoad.progress > .9f) continue;
            _loadSceneRoutine = null;
            if (_showLoadingScreenRoutine == null)
                StartCoroutine(HideLoadingScreen());
            else
                shouldHideLoadingScreen = true;
            break;
        }
    }

    private IEnumerator ShowLoadingScreen()
    {
        
        ((RectTransform)_loadingPane.transform)
            .SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Camera.main.pixelWidth / transform.localScale.x);
        ((RectTransform) _loadingPane.transform)
            .SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Camera.main.pixelHeight / transform.localScale.x);
        
        float elapsedTime = 0;
        float loadingTime = 1f;
        while (elapsedTime < loadingTime)
        {
            elapsedTime += Time.deltaTime;
            _loadingPane.transform.position = Vector3.Slerp(Vector3.zero, new Vector3(Camera.main.pixelWidth, 0, 0), elapsedTime / loadingTime);
            // TODO: Lerp loading menu onto viewport
            yield return null;
        }

        if (shouldHideLoadingScreen)
            _hideLoadingScreenRoutine = StartCoroutine(HideLoadingScreen());

        _showLoadingScreenRoutine = null;
        ((RectTransform)_loadingPane.transform).anchoredPosition = new Vector3(Camera.main.pixelWidth, 0, 0);
    }

    private IEnumerator HideLoadingScreen()
    {
        float elapsedTime = 0;
        float loadingTime = 1f;
        while (elapsedTime < loadingTime)
        {
            elapsedTime += Time.deltaTime;
            ((RectTransform)_loadingPane.transform).anchoredPosition = Vector3.Slerp(new Vector3(Camera.main.pixelWidth, 0, 0), Vector3.zero, elapsedTime / loadingTime);
            // TODO: Lerp loading menu onto viewport
            yield return null;
        }

        _asyncLoad.allowSceneActivation = true;
        _hideLoadingScreenRoutine = null;
        _loadingPane.transform.position = Vector3.zero;
        GetComponent<Canvas>().enabled = false;
    }
}
