using UnityEngine;

public class AimingPoint : MonoBehaviour
{
    //Management
    private UIManager _uiManager;
    
    private RectTransform _rectTrm;
    
    private void Awake()
    {
        _uiManager = UIManager.Instance;
        _rectTrm = transform as RectTransform;
    }

    private void Start()
    {
        GameManager.Instance.InputReader.MoveMouseEvent += AimingMove;
    }
    
    private void AimingMove(Vector2 position)
    {
        position = _uiManager.ScreenConvertToCanvasSpace(position);
        _rectTrm.anchoredPosition = position;
    }
}
