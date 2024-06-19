using UnityEngine;

public class LobbySceneGameManager : MonoSingleton<LobbySceneGameManager>
{
    private void Awake()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Confined;
        
        ScreenFadeManager.Instance.Fade(true, 1f);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
