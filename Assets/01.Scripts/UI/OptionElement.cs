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

        foreach (var button in transform.GetComponentsInChildren<Button>())
        {
            button.onClick.AddListener(HandleClick);
        }

        foreach (var toggle in transform.GetComponentsInChildren<Toggle>())
        {
            toggle.onValueChanged.AddListener(x => HandleClick());
        }
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