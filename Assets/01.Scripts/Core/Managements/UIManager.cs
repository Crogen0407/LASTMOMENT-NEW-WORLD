using TMPro;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    [field: SerializeField] public Camera UICamera;
    [SerializeField] private Canvas _canvas;
    public SettingOptionDataSO SettingOptionData;
    [SerializeField] private TextMeshProUGUI _settingDescriptionText;
    
    private readonly float _width = Screen.width;
    private readonly float _height = Screen.height;
    
    public void Init()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public Vector2 ScreenConvertToCanvasSpace(Vector2 position)
    {
        Vector2 canvasRectSize = ((RectTransform)_canvas.transform).rect.size;
        position.x = MathExtension.Remap(position.x, 0, _width, -canvasRectSize.x * 0.5f, canvasRectSize.x * 0.5f);
        position.y = MathExtension.Remap(position.y, 0, _height, -canvasRectSize.y * 0.5f, canvasRectSize.y * 0.5f);
        
        return position;
    }

    public void SetSettingDescription(SettingOptionType settingOptionType)
    {
        _settingDescriptionText.text = SettingOptionData.uiDescriptionDictionary[settingOptionType];
    }
} 
