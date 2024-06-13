using UnityEngine;
using UnityEngine.UI;

public class PauseMenuContent : MonoBehaviour
{
    //Managements
    private UIManager _uiManager;
    private GameSettingManager _gameSettingManager;
    private GameManager _gameManager;
    
    [SerializeField] private Button _openSettingButton;
    [SerializeField] private Button _returnButton;
    [SerializeField] private Button _quitGameButton;

    private void Awake()
    {
        _gameManager = GameManager.Instance;
        _gameSettingManager = GameSettingManager.Instance;
        _uiManager = UIManager.Instance;
        
        //Events
        _openSettingButton.onClick.AddListener(_gameSettingManager.OpenSettingWindow);
        _returnButton.onClick.AddListener(_uiManager.ClosePauseWindow);
        _quitGameButton.onClick.AddListener(_gameManager.GotoLobbyScene);
    }
}
