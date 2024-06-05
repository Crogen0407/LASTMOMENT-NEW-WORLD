using Crogen.JsamJson;
using Crogen.PowerfulInput;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    public int currentGold=0;
    public int currentPreamble=0;
    
    [field:SerializeField] public InputReader InputReader { get; private set; }
    [field:SerializeField] public Player Player { get; private set; }
    
    private void Awake()
    {
        InputReader.MouseClickEvent += UIManager.Instance.Init;
        InputReader.EscEvent += UIManager.Instance.OpenPauseWindow;
    }

    private void OnDestroy()
    {
        InputReader.MouseClickEvent -= UIManager.Instance.Init;
        InputReader.EscEvent -= UIManager.Instance.OpenPauseWindow;
    }

    [ContextMenu("SAVEGAMERESULT")]
    public void SaveGameResult()
    {
        GameData gameData = JsamJson.Load<GameData>(false);
        gameData.gold += currentGold;
        gameData.preamble += currentPreamble;
        JsamJson.Save<GameData>(gameData, false);
    }
    
    //Debug    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            TalkContent.Instance.OnTalk("System", "누군가 말했다.");
        }
    }

    #region SceneMangement

    public void GotoLobbyScene()
    {
        SceneManager.LoadScene(SceneNames.LobbyScene);
    }

    #endregion
    
}