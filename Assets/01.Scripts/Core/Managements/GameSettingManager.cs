using System;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class GameSettingManager : MonoSingleton<GameSettingManager>
{
    [Header("SettingUI")]
    public Canvas settingCanvas;

    [SerializeField] private ArrowNumberInput xSensitivityInput;
    [SerializeField] private ArrowNumberInput ySensitivityInput;
    
    [SerializeField] private ArrowNumberInput masterVolumeInput;
    [SerializeField] private ArrowNumberInput bgmInput;
    [SerializeField] private ArrowNumberInput sfxInput;
    
    [SerializeField] private ArrowListInput imageQualityInput;
    [SerializeField] private ArrowListInput fpsInput;
    [SerializeField] private Toggle windowModeInput;
    
    public SettingOptionDataSO SettingOptionData;
    [SerializeField] private TextMeshProUGUI _settingDescriptionText;
    [SerializeField] private AudioMixer _audioMixer;
    
    //Managements
    private GameDataManager _gameDataManager;
    
    public event Action OnSettingDataLoadEvent;

    [SerializeField] private RenderPipelineAsset[] _pipelines;

    private void Awake()
    {
        xSensitivityInput.onClickEvent.AddListener(HandleXSensitivity);
        ySensitivityInput.onClickEvent.AddListener(HandleYSensitivity);
        
        masterVolumeInput.onClickEvent.AddListener(HandleMasterVolume);
        bgmInput.onClickEvent.AddListener(HandleBGM);
        sfxInput.onClickEvent.AddListener(HandleSFX);
        
        imageQualityInput.onClickEvent.AddListener(HandleImageQuality);
        fpsInput.onClickEvent.AddListener(HandleFPS);
        windowModeInput.onValueChanged.AddListener(HandleWindowMode);
    }

    private void OnDestroy()
    {
        xSensitivityInput.onClickEvent?.RemoveAllListeners();
        ySensitivityInput.onClickEvent?.RemoveAllListeners();
        
        masterVolumeInput.onClickEvent?.RemoveAllListeners();
        bgmInput.onClickEvent?.RemoveAllListeners();
        sfxInput.onClickEvent?.RemoveAllListeners();
        
        imageQualityInput.onClickEvent?.RemoveAllListeners();
        fpsInput.onClickEvent?.RemoveAllListeners();
        windowModeInput.onValueChanged?.RemoveAllListeners();
    }

    private void Start()
    {
        _gameDataManager = GameDataManager.Instance;
        _gameDataManager.LoadData();
        LoadSetting();
    }

    #region SettingUI

    public void OpenSettingWindow()
    {
        settingCanvas.gameObject.SetActive(true);
        _gameDataManager.LoadData();
        LoadSetting();
        
        //UI Init
        xSensitivityInput.SetValue(_gameDataManager.SettingArray[(int)SettingOptionType.XSensitivity]);
        ySensitivityInput.SetValue(_gameDataManager.SettingArray[(int)SettingOptionType.YSensitivity]);
        
        masterVolumeInput.SetValue(_gameDataManager.SettingArray[(int)SettingOptionType.MasterVolume]);
        bgmInput.SetValue(_gameDataManager.SettingArray[(int)SettingOptionType.BGM]);
        sfxInput.SetValue(_gameDataManager.SettingArray[(int)SettingOptionType.SFX]);
        
        imageQualityInput.SetValue(_gameDataManager.SettingArray[(int)SettingOptionType.ImageQuality]);
        fpsInput.SetValue(_gameDataManager.SettingArray[(int)SettingOptionType.FPS]);
        windowModeInput.isOn = Convert.ToBoolean(_gameDataManager.SettingArray[(int)SettingOptionType.WindowMode]);
    }
    
    public void CloseSettingWindow()
    {
        LoadSetting();
        settingCanvas.gameObject.SetActive(false);
        _gameDataManager.SaveData();
    }

    private void HandleXSensitivity(int value)
    {
        ApplySetting(SettingOptionType.XSensitivity, value);
    }
    
    private void HandleYSensitivity(int value)
    {
        ApplySetting(SettingOptionType.YSensitivity, value);
    }

    private void HandleMasterVolume(int value)
    {
        ApplySetting(SettingOptionType.MasterVolume, value);
        if(value!=0)
            _audioMixer.SetFloat("Master", Mathf.Log10(value*0.1f) * 20);
        else
            _audioMixer.SetFloat("Master", -80);
    }
    
    private void HandleBGM(int value)
    {
        ApplySetting(SettingOptionType.BGM, value);
        if(value!=0)
            _audioMixer.SetFloat("BGM", Mathf.Log10(value*0.1f) * 20);
        else
            _audioMixer.SetFloat("BGM", -80);
    }
    
    private void HandleSFX(int value)
    {
        ApplySetting(SettingOptionType.SFX, value);
        if(value!=0)
            _audioMixer.SetFloat("SFX", Mathf.Log10(value*0.1f) * 20);
        else
            _audioMixer.SetFloat("SFX", -80);
    }
    
    private void HandleImageQuality(int value)
    {
        ApplySetting(SettingOptionType.ImageQuality, value);
    }

    private void HandleFPS(int value)
    {
        ApplySetting(SettingOptionType.FPS, value);
    }
    
    private void HandleWindowMode(bool value)
    {
        ApplySetting(SettingOptionType.WindowMode, Convert.ToInt32(value));
    }
    
    #endregion
    
    public void ApplySetting(SettingOptionType settingOptionType, int value)
    {
        _gameDataManager.SettingArray[(int)settingOptionType] = value;
    }

    public void LoadSetting()
    {
        if(_gameDataManager.SettingArray.Length == 0)
        {
            _gameDataManager.SettingArray = new GameData().settingArray;
            _gameDataManager.SaveData();
        }
        OnSettingDataLoadEvent?.Invoke();
        
        //게임 적용
        //소리 적용
        if(_gameDataManager.SettingArray[(int)SettingOptionType.MasterVolume]!=0)
            _audioMixer.SetFloat("Master", Mathf.Log10(_gameDataManager.SettingArray[(int)SettingOptionType.MasterVolume]*0.1f) * 20);
        else
            _audioMixer.SetFloat("Master", -80);
        
        if(_gameDataManager.SettingArray[(int)SettingOptionType.BGM]!=0)
            _audioMixer.SetFloat("BGM", Mathf.Log10(_gameDataManager.SettingArray[(int)SettingOptionType.BGM]*0.1f) * 20);
        else
            _audioMixer.SetFloat("BGM", -80);
        
        if(_gameDataManager.SettingArray[(int)SettingOptionType.MasterVolume]!=0)
            _audioMixer.SetFloat("SFX", Mathf.Log10(_gameDataManager.SettingArray[(int)SettingOptionType.SFX]*0.1f) * 20);
        else
            _audioMixer.SetFloat("SFX", -80);
        #region Graphic
        
        //ImageQuality
        QualitySettings.SetQualityLevel(_gameDataManager.SettingArray[(int)SettingOptionType.ImageQuality]);
        QualitySettings.renderPipeline = _pipelines[_gameDataManager.SettingArray[(int)SettingOptionType.ImageQuality]];
            
        //FPS
        int fpsValue = 30;
        switch (_gameDataManager.SettingArray[(int)SettingOptionType.FPS])
        {
            case 0: fpsValue = 30; break;
            case 1: fpsValue = 60; break;
            case 2: fpsValue = 120; break;
            case 3: fpsValue = 144; break;
        }
        Application.targetFrameRate = fpsValue;
        
        //WindowMode
        int windowModeValue = _gameDataManager.SettingArray[(int)SettingOptionType.WindowMode];
        Screen.fullScreenMode = windowModeValue == 0 ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;


        #endregion

    }
    
    public void SetSettingDescription(SettingOptionType settingOptionType)
    {
        _settingDescriptionText.text = SettingOptionData.uiDescriptionDictionary[settingOptionType];
    }
}