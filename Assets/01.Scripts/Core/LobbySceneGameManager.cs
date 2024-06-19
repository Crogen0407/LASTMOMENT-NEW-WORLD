using UnityEngine;

public class LobbySceneGameManager : MonoSingleton<LobbySceneGameManager>
{
    public void QuitGame()
    {
        Application.Quit();
    }
}
