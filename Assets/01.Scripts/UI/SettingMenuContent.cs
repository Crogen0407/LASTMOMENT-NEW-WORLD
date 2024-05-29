using UnityEngine;
using UnityEngine.UI;

public class SettingMenuContent : MonoBehaviour
{
    //Managements
    private UIManager _uiManager;
    
    [SerializeField] private Button[] _settingElementButtons;
    [SerializeField] private RectTransform[] _settingPanels;

    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _returnButton;
    
    private void Awake()
    {
        _uiManager = UIManager.Instance;
        for (int i = 0; i < _settingElementButtons.Length; ++i)
        {
            int index = i;
            _settingElementButtons[i].onClick.AddListener(() =>
            {
                ChangeTap(index);
            });
        }
        
        _exitButton.onClick.AddListener(_uiManager.CloseSettingWindow);
    }

    private void ChangeTap(int btnIndex)
    {
        for (int i = 0; i < _settingElementButtons.Length; ++i)
        {
            _settingPanels[i].gameObject.SetActive(btnIndex==i);
        }
    }
}
