using UnityEngine;
using UnityEngine.UI;

public class OptionElement : MonoBehaviour
{
    //Managements
    private UIManager _uiManager;
    
    //Components
    private Button _button;

    [SerializeField] private SettingOptionType _settingOptionType;

    private void Awake()
    {
        //Managements
        _uiManager = UIManager.Instance;
        
        //Components
        _button = GetComponent<Button>();
        _button.onClick.AddListener(HandleClick);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(HandleClick);
    }

    private void HandleClick()
    {
        _uiManager.SetSettingDescription(_settingOptionType);
    }
}