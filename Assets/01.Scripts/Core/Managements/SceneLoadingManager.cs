using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoadingManager : MonoSingleton<SceneLoadingManager>
{
    [SerializeField] private GameObject LoadingScreenPrefab;
    private GameObject LoadingScreen;
    private Image LoadingBarFill;
    
    private int _sceneIndex;
    private string _sceneName;

    public void LoadingScene(string sceneName, float fadeDuration = 1f)
    {
        ScreenFadeManager.Instance.Fade(false, fadeDuration, () =>
        {
            _sceneName = sceneName;
            if (LoadingScreen == null)
            {
                LoadingScreen = Instantiate(LoadingScreenPrefab);
                LoadingBarFill = LoadingScreen.transform.Find("LoadingBar/Fill").GetComponent<Image>();
            }
            else
            {
                Destroy(LoadingScreen);
            }
            StartCoroutine(CoroutineLoadingScene(sceneName));
            SceneManager.sceneLoaded += SceneLoadComplete;
        });
    }

    IEnumerator CoroutineLoadingScene(string sceneName)
    {
        LoadingBarFill.fillAmount = 0f;
        LoadingScreen.SetActive(true);
        
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        float timer = 0;
        while (!operation.isDone)
        {
            yield return null;

            if (operation.progress < 0.9f)
            {
                LoadingBarFill.fillAmount = operation.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                LoadingBarFill.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
                if (LoadingBarFill.fillAmount >= 1f)
                {
                    operation.allowSceneActivation = true;
                    //LoadingScreen.SetActive(false);
                    yield break;
                }
            }
        }
    }
    private void SceneLoadComplete(Scene arg0, LoadSceneMode arg1)
    {
        if (_sceneIndex == arg0.buildIndex)
        {
            ScreenFadeManager.Instance.Fade(true, 1, () =>
            {
                Time.timeScale = 1;
                SceneManager.sceneLoaded -= SceneLoadComplete;
            });
        }
    }
}
