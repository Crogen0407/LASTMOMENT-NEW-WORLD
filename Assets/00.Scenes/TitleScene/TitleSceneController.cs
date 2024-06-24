using UnityEngine;

public class TitleSceneController : MonoBehaviour
{
    private bool _isGameStart = false;
    
    void Update()
    {
        if (Input.anyKeyDown && _isGameStart == false)
        {
            _isGameStart = true;
            SceneLoadingManager.Instance.LoadingScene(SceneNames.LobbyScene);
        }
    }
}
