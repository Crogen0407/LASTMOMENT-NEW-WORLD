using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TargetPointContent : MonoSingleton<TargetPointContent>
{
    [field:SerializeField] public bool IsActive { get; set; }
    
    private StageManager _stageManager;
    
    private Transform _playerCameraTrm;
    private Transform _playerTrm;    
    
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _distanceText;
    
    private void Awake()
    {
        _stageManager = StageManager.Instance;
        
        _playerCameraTrm = GameObject.Find("PlayerVirtualCamera").transform;
        _playerTrm = GameManager.Instance.Player.transform;
    }

    private void FixedUpdate()
    {
        IsActive = Mathf.Approximately(Time.timeScale, 0) == false && !_stageManager.gameClear;
        _image.gameObject.SetActive(_stageManager.currentTargetTrm != null && IsActive);
        if (_stageManager.currentTargetTrm == null) return;
        float minX = _image.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;
        
        float minY = _image.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.height - minY;
        
        Vector2 pos = Camera.main.WorldToScreenPoint(_stageManager.currentTargetTrm.position);

        if (Vector3.Dot((_stageManager.currentTargetTrm.position - _playerCameraTrm.position),  _playerCameraTrm.forward) < 0)
        {
            if (pos.x < Screen.width / 2)
            {
                pos.x = maxX;
            }
            else
            {
                pos.x = minX;
            }
        }

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        _distanceText.text = $"{(int)Vector3.Distance(_stageManager.currentTargetTrm.position, _playerTrm.position)}m";
        
        _image.transform.position = pos;
    }
}
