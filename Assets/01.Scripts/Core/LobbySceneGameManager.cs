using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbySceneGameManager : MonoSingleton<LobbySceneGameManager>
{
    public void QuitGame()
    {
        Application.Quit();
    }
}
