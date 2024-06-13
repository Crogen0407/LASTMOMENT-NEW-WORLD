using UnityEngine;
using UnityEngine.UI;

public class SettingMenuContent : MonoBehaviour
{
    //Managements
    private GameSettingManager _gameSettingManager;
    
    [SerializeField] private Button[] _settingElementButtons;
    [SerializeField] private RectTransform[] _settingPanels;

    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _returnButton;
    
    private void Awake()
    {
        _gameSettingManager = GameSettingManager.Instance;
        for (int i = 0; i < _settingElementButtons.Length; ++i)
        {
            int index = i;
            _settingElementButtons[i].onClick.AddListener(() =>
            {
                ChangeTap(index);
            });
        }
        
        _exitButton.onClick.AddListener(_gameSettingManager.CloseSettingWindow);
    }

    private void OnEnable()
    {
        for (int i = 0; i < _settingPanels.Length; ++i)
        {
            if (i == 0)
            {
                _settingPanels[i].gameObject.SetActive(true);
                continue;
            }
            _settingPanels[i].gameObject.SetActive(false);
        }
    }

    private void ChangeTap(int btnIndex)
    {
        for (int i = 0; i < _settingElementButtons.Length; ++i)
        {
            _settingPanels[i].gameObject.SetActive(btnIndex==i);
        }
    }
}
