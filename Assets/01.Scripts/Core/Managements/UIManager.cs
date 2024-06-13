using System;
using AYellowpaper.SerializedCollections;
using Crogen.HealthSystem;
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
    public Canvas pauseCanvas;
    public Canvas aimCanvas;
    
    private readonly float _width = Screen.width;
    private readonly float _height = Screen.height;

    private bool _isPause = false;

    [Header("BossUI")] 
    [SerializeField] private Slider _bossHealthBarl;
    [SerializeField] private TextMeshProUGUI _bossNameText;
    private HealthSystem _bossHealthSystem;
    
    private void Awake()
    {
        _gameSettingManager = GameSettingManager.Instance;
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

    public void ShowBossUI(Boss boss)
    {
        _bossHealthSystem = boss.HealthSystem;
        _bossNameText.text = boss.enemyType.ToString().Replace('_', '-');
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
} 
