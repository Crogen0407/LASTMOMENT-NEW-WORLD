using TMPro;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    [field: SerializeField] public Camera UICamera;
    public SettingOptionDataSO SettingOptionData;
    [SerializeField] private TextMeshProUGUI _settingDescriptionText;
    
    [Header("Canvas")]
    //Canvas
    [SerializeField] private Canvas gameCanvas;
    [SerializeField] private Canvas settingCanvas;
    [SerializeField] private Canvas pauseCanvas;
    
    private readonly float _width = Screen.width;
    private readonly float _height = Screen.height;

    private bool _isPause = false;
    
    public void Init()
    {
        if (_isPause)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            return;
        }
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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
        settingCanvas.gameObject.SetActive(true);
    }
    
    public void CloseSettingWindow()
    {
        settingCanvas.gameObject.SetActive(false);
    }

    #endregion
} 
