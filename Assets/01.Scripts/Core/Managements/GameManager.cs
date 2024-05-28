using Crogen.JsamJson;
using Crogen.PowerfulInput;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public int currentGold=0;
    public int currentPreamble=0;
    
    [field:SerializeField] public InputReader InputReader { get; private set; }
    
    private void Awake()
    {
        //InputReader.MouseClickEvent += UIManager.Instance.Init;
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
}