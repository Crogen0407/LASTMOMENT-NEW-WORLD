using System;
using AYellowpaper.SerializedCollections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    //Managements
    private GameSettingManager _gameSettingManager;
    
    [field: SerializeField] public Camera UICamera;
    public SettingOptionDataSO SettingOptionData;
    [SerializeField] private TextMeshProUGUI _settingDescriptionText;

    [Header("Item")] 
    [SerializeField] private Image[] _itemIcons;
    [SerializeField] private SerializedDictionary<ItemType, Sprite> _itemSpriteDictionary;
    private Sprite _emptyImage;
    
    [Header("Canvas")]
    public Canvas gameCanvas;
    public Canvas settingCanvas;
    public Canvas pauseCanvas;
    public Canvas aimCanvas;
    
    [Header("SettingUI")]
    [SerializeField] private ArrowNumberInput xSensitivityInput;
    [SerializeField] private ArrowNumberInput ySensitivityInput;
    
    [SerializeField] private ArrowNumberInput masterVolumeInput;
    [SerializeField] private ArrowNumberInput bgmInput;
    [SerializeField] private ArrowNumberInput sfxInput;
    
    [SerializeField] private ArrowListInput imageQualityInput;
    [SerializeField] private ArrowListInput fpsInput;
    [SerializeField] private Toggle windowModeInput;
    
    private readonly float _width = Screen.width;
    private readonly float _height = Screen.height;

    private bool _isPause = false;

    private void Awake()
    {
        _gameSettingManager = GameSettingManager.Instance;
        
        xSensitivityInput.onClickEvent.AddListener(HandleXSensitivity);
        ySensitivityInput.onClickEvent.AddListener(HandleYSensitivity);
        
        masterVolumeInput.onClickEvent.AddListener(HandleMasterVolume);
        bgmInput.onClickEvent.AddListener(HandleBGM);
        sfxInput.onClickEvent.AddListener(HandleSFX);
        
        imageQualityInput.onClickEvent.AddListener(HandleImageQuality);
        fpsInput.onClickEvent.AddListener(HandleFPS);
        windowModeInput.onValueChanged.AddListener(HandleWindowMode);

        _emptyImage = _itemIcons[0].sprite;
    }

    public void Init()
    {
        if (!_isPause)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = _isPause;
    }

    public Vector2 ScreenConvertToCanvasSpace(Vector2 position)
    {
        Vector2 canvasRectSize = ((RectTransform)gameCanvas.transform).rect.size;
        position.x = MathExtension.Remap(position.x, 0, _width, -canvasRectSize.x * 0.5f, canvasRectSize.x * 0.5f);
        position.y = MathExtension.Remap(position.y, 0, _height, -canvasRectSize.y * 0.5f, canvasRectSize.y * 0.5f);
        
        return position;
    }

    public void SetSettingDescription(SettingOptionType settingOptionType)
    {
        _settingDescriptionText.text = SettingOptionData.uiDescriptionDictionary[settingOptionType];
    }

    #region Item

    public void UpdateItemIcon(int iconIndex, ItemType itemType)
    {
        if (itemType == ItemType.None)
        {
            _itemIcons[iconIndex].sprite = _emptyImage;
        }
        else
        {
            _itemIcons[iconIndex].sprite = _itemSpriteDictionary[itemType];
        }

    }

    #endregion

    #region PauseWindow

    public void OpenPauseWindow()
    {
        _isPause = true;
        Time.timeScale = 0;
        pauseCanvas.gameObject.SetActive(true);
    }

    public void ClosePauseWindow()
    {
        _isPause = false;
        Init();
        Time.timeScale = 1;
        pauseCanvas.gameObject.SetActive(false);
    }

    #endregion

    #region SettingWindow

    public void OpenSettingWindow()
    {
        _gameSettingManager = GameSettingManager.Instance;

        settingCanvas.gameObject.SetActive(true);
        _gameSettingManager.LoadSetting();
        
        //UI Init
        xSensitivityInput.SetValue(_gameSettingManager.SettingArray[(int)SettingOptionType.XSensitivity]);
        ySensitivityInput.SetValue(_gameSettingManager.SettingArray[(int)SettingOptionType.YSensitivity]);
        
        masterVolumeInput.SetValue(_gameSettingManager.SettingArray[(int)SettingOptionType.MasterVolume]);
        bgmInput.SetValue(_gameSettingManager.SettingArray[(int)SettingOptionType.BGM]);
        sfxInput.SetValue(_gameSettingManager.SettingArray[(int)SettingOptionType.SFX]);
        
        imageQualityInput.SetValue(_gameSettingManager.SettingArray[(int)SettingOptionType.ImageQuality]);
        fpsInput.SetValue(_gameSettingManager.SettingArray[(int)SettingOptionType.FPS]);
        windowModeInput.isOn = Convert.ToBoolean(_gameSettingManager.SettingArray[(int)SettingOptionType.WindowMode]);
    }
    
    public void CloseSettingWindow()
    {
        settingCanvas.gameObject.SetActive(false);
        _gameSettingManager.SaveSetting();
    }

    #endregion
    
    #region Input Handler
    private void HandleXSensitivity(int value)
    {
        _gameSettingManager.ApplySetting(SettingOptionType.XSensitivity, value);
    }
    
    private void HandleYSensitivity(int value)
    {
        _gameSettingManager.ApplySetting(SettingOptionType.YSensitivity, value);
    }

    private void HandleMasterVolume(int value)
    {
        _gameSettingManager.ApplySetting(SettingOptionType.MasterVolume, value);
    }
    
    private void HandleBGM(int value)
    {
        _gameSettingManager.ApplySetting(SettingOptionType.BGM, value);
    }
    
    private void HandleSFX(int value)
    {
        _gameSettingManager.ApplySetting(SettingOptionType.SFX, value);
    }
    
    private void HandleImageQuality(int value)
    {
        _gameSettingManager.ApplySetting(SettingOptionType.ImageQuality, value);
    }

    private void HandleFPS(int value)
    {
        _gameSettingManager.ApplySetting(SettingOptionType.FPS, value);
    }
    
    private void HandleWindowMode(bool value)
    {
        _gameSettingManager.ApplySetting(SettingOptionType.WindowMode, Convert.ToInt32(value));
    }
    #endregion
} 
