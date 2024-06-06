using System;
using System.Collections;
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
        ScreenFade.Instance.Fade(true, 1f);
        
        InputReader.DisablePlayerActions();
    }

    private void Start()
    {
        StartCoroutine(GameStart());
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            SceneLoadingManager.Instance.LoadingScene(SceneUtility.GetBuildIndexByScenePath(SceneNames.TitleScene));
        }
    }

    #region SceneMangement

    public void GotoLobbyScene()
    {
        SceneManager.LoadScene(SceneNames.LobbyScene);
    }

    #endregion

    private IEnumerator GameStart()
    {
        InputReader.DisablePlayerActions();
        Player.Movement.HandleSpeedChange(true);
        yield return new WaitForSeconds(10);
        InputReader.EnablePlayerActions();
        Player.Movement.HandleSpeedChange(false);
    }
}