using System;
using Crogen.JsamJson;
using UnityEngine;

public class GameSettingManager : MonoSingleton<GameSettingManager>
{
    [field:SerializeField] public int[] SettingArray { get; private set; }
    public event Action OnSettingDataLoadEvent;
    
    private void Start()
    {
        LoadSetting();
    }

    private void Reset()
    {
        Array arr = Enum.GetValues(typeof(SettingOptionType));
        SettingArray = new int[arr.Length];
    }

    public void ApplySetting(SettingOptionType settingOptionType, int value)
    {
        SettingArray[(int)settingOptionType] = value;
    }
    
    public void SaveSetting()
    {
        GameData gameData = JsamJson.Load<GameData>(false);
        string path = JsamJson.Save<GameData>(new GameData
        {
            gold = gameData.gold,
            preamble = gameData.preamble,
            settingArray = SettingArray
        }, false, true);
    }

    public void LoadSetting()
    {
        OnSettingDataLoadEvent?.Invoke();
        GameData gameData = JsamJson.Load<GameData>(false);
        if(gameData.settingArray != null)
                SettingArray = gameData.settingArray;
    }
}