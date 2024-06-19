using UnityEngine;
using UnityEngine.UI;

public class OptionElement : MonoBehaviour
{
    //Managements
    private GameSettingManager _gameSettingManager;
    
    //Components
    private Button _button;

    [SerializeField] private SettingOptionType _settingOptionType;
    
    private void Awake()
    {
        //Managements
        _gameSettingManager = GameSettingManager.Instance;
        
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
        _gameSettingManager.SetSettingDescription(_settingOptionType);
    }
}