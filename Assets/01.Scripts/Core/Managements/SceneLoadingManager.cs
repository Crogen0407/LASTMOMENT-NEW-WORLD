using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoadingManager : MonoDontDestroySingleton<SceneLoadingManager>
{
    [SerializeField] private GameObject LoadingScreenPrefab;
    [SerializeField] private GameObject LoadingScreen;
    [SerializeField] private Image LoadingBarFill;
    private int _sceneIndex;
    public void LoadingScene(int sceneID)
    {
        ScreenFade.Instance.Fade(false, 1, () =>
        {
            _sceneIndex = sceneID;
            if (LoadingScreen == null)
            {
                LoadingScreen = Instantiate(LoadingScreenPrefab);
                LoadingBarFill = LoadingScreen.transform.Find("LoadingBar/Fill").GetComponent<Image>();
                DontDestroyOnLoad(LoadingScreen);
            }
            else
            {
                Destroy(LoadingScreen);
            }
            SceneManager.sceneLoaded += SceneLoadComplete;
            StartCoroutine(CoroutineLoadingScene(sceneID));
        });
    }

    IEnumerator CoroutineLoadingScene(int sceneID)
    {
        LoadingBarFill.fillAmount = 0f;
        LoadingScreen.SetActive(true);
        
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);
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
                    LoadingScreen.SetActive(false);
                    yield break;
                }
            }
        }
    }

    private void SceneLoadComplete(Scene arg0, LoadSceneMode arg1)
    {
        if (_sceneIndex == arg0.buildIndex)
        {
            ScreenFade.Instance.Fade(true, 1, () =>
            {
            });
            SceneManager.sceneLoaded -= SceneLoadComplete;
        }
    }
}
