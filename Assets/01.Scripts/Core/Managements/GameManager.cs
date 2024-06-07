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
            GameClear();
        }
    }


    public void GameOver()
    {
        UIManager.Instance.gameCanvas.gameObject.SetActive(false);
        UIManager.Instance.aimCanvas.gameObject.SetActive(false);
        Time.timeScale = 0;
        float startValue = 0;
        float endValue = -100;
        Sequence seq = DOTween.Sequence();
        seq.Append(DOTween.To(() => startValue, value => ColorAdjustments.saturation.value = value, endValue, 1)).SetUpdate(true);
        seq.AppendInterval(1f).SetUpdate(true);
        seq.AppendCallback(() =>
        {
            SceneLoadingManager.Instance.LoadingScene(SceneNames.LobbyScene);
        }).SetUpdate(true);
    }

    
    public void GameClear()
    {
        TalkContent.Instance.OnTalk("System", "작전 성공", () =>
        {
            TalkContent.Instance.OnTalk("System", "ST-091은 본부로 귀환할 것을 요청합니다", () =>
            {
                TalkContent.Instance.OnTalk("System", "수락됨", 2, null, () =>
                {
                    TalkContent.Instance.OnTalk("System", "ST-091, 본부로 귀환합니다", 1, null, () =>
                    {
                        ScreenFadeManager.Instance.Fade(false, 5, () =>
                        {
                            SceneLoadingManager.Instance.LoadingScene(SceneNames.LobbyScene);
                        });
                    });
                });
            });
        });
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