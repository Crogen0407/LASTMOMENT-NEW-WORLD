using System;
using Crogen.JsamJson;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class GameSettingManager : MonoSingleton<GameSettingManager>
{
    [field:SerializeField] public int[] SettingArray { get; private set; }
    public event Action OnSettingDataLoadEvent;

    [SerializeField] private RenderPipelineAsset[] _pipelines;
    
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
        gameData.settingArray = SettingArray;
        JsamJson.Save<GameData>(gameData, false, true);
    }

    public void LoadSetting()
    {
        OnSettingDataLoadEvent?.Invoke();
        GameData gameData = JsamJson.Load<GameData>(false);
        if(gameData.settingArray != null)
                SettingArray = gameData.settingArray;
        
        //게임 적용
        //소리 적용

        #region Graphic
        
        //ImageQuality
        QualitySettings.SetQualityLevel(SettingArray[(int)SettingOptionType.ImageQuality]);
        QualitySettings.renderPipeline = _pipelines[SettingArray[(int)SettingOptionType.ImageQuality]];
            
        //FPS
        int fpsValue = 30;
        switch (SettingArray[(int)SettingOptionType.FPS])
        {
            case 0: fpsValue = 30; break;
            case 1: fpsValue = 60; break;
            case 2: fpsValue = 120; break;
            case 3: fpsValue = 144; break;
        }
        Application.targetFrameRate = fpsValue;
        
        //WindowMode
        int windowModeValue = SettingArray[(int)SettingOptionType.WindowMode];
        Screen.fullScreenMode = windowModeValue == 0 ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;


        #endregion

    }
}