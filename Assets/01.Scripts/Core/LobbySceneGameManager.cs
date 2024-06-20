using UnityEngine;

public class LobbySceneGameManager : MonoSingleton<LobbySceneGameManager>
{
    private void Awake()
    {
        GameDataManager.Instance.LoadData();
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    private void Start()
    {
        ScreenFadeManager.Instance.Fade(true, 1f);
        SoundManager.Instance.PlayBGM(true, 0.5f, 4f,1);    
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
