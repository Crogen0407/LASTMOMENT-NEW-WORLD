using System.Collections;
using Crogen.JsamJson;
using Crogen.PowerfulInput;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    public int currentGold=0;
    public int currentPreamble=0;
    
    [field:SerializeField] public InputReader InputReader { get; private set; }
    [field:SerializeField] public Player Player { get; private set; }

    [Header("Scene")] 
    [SerializeField] private string _gameOverScene = SceneNames.LobbyScene;
    
    [Header("PP")]
    [field:SerializeField] public Volume Volum;
    [HideInInspector] public ColorAdjustments ColorAdjustments;
    
    private void Awake()
    {
        InputReader.MouseClickEvent += UIManager.Instance.Init;
        InputReader.EscEvent += UIManager.Instance.OpenPauseWindow;
        
        if(Volum.profile.TryGet<ColorAdjustments>(out ColorAdjustments ca))
        {
            ColorAdjustments = ca;
        }        
        
        ScreenFadeManager.Instance.Fade(true, 1f);
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
        JsamJson.Save<GameData>(gameData, false, false);
    }

    public void GameOver()
    {
        UIManager.Instance.gameCanvas.gameObject.SetActive(false);
        UIManager.Instance.aimCanvas.gameObject.SetActive(false);
        
        float startValue = 0;
        float endValue = -100;
        Sequence seq = DOTween.Sequence();
        
        seq.AppendInterval(1f).SetUpdate(true);
        seq.Append(DOTween.To(() => startValue, value => ColorAdjustments.saturation.value = value, endValue, 1)).SetUpdate(true);
        seq.AppendCallback(() =>
        {
            Time.timeScale = 0;
        }).SetUpdate(true);
        seq.AppendInterval(1f).SetUpdate(true);
        seq.AppendCallback(() =>
        {
            SceneLoadingManager.Instance.LoadingScene(_gameOverScene);
        }).SetUpdate(true);
    }
    
    #region SceneMangement

    public void GotoLobbyScene()
    {
        SceneLoadingManager.Instance.LoadingScene(SceneNames.LobbyScene);
    }

    #endregion

    private IEnumerator GameStart()
    {
        TargetPointContent.Instance.IsActive = false;
        InputReader.DisablePlayerActions();
        Player.Movement.HandleSpeedChange(true);
        yield return new WaitForSeconds(5);
        TargetPointContent.Instance.IsActive = true;
        InputReader.EnablePlayerActions();
        Player.Movement.HandleSpeedChange(false);
    }
}