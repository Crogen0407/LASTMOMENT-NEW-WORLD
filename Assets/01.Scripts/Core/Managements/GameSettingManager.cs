using System;
using Crogen.JsamJson;
using UnityEngine;

public class GameSettingManager : MonoSingleton<GameSettingManager>
{
    [field:SerializeField] public int[] SettingArray { get; private set; }

    private void Reset()
    {
        Array arr = Enum.GetValues(typeof(SettingOptionType));
        SettingArray = new int[arr.Length];
    }

    public void SaveSetting()
    {
        GameData gameData = JsamJson.Load<GameData>();
        JsamJson.Save<GameData>(new GameData
        {
            gold = gameData.gold,
            preamble = gameData.preamble,
            settingArray = SettingArray
        });
    }

    public void LoadSetting()
    {
        GameData gameData = JsamJson.Load<GameData>();
        SettingArray = gameData.settingArray;
    }
}
