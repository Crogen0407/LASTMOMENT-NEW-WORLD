using UnityEngine;

public class TitleSceneController : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneLoadingManager.Instance.LoadingScene(SceneNames.LobbyScene);
        }
    }
}
