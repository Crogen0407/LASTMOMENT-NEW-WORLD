using UnityEngine;

public class AimingPoint : MonoBehaviour
{
    private RectTransform _rectTrm;

    private void Awake()
    {
        _rectTrm = transform as RectTransform;
    }

    private void Start()
    {
        GameManager.Instance.InputReader.MoveMouseEvent += AimingMove;
    }
    
    private void AimingMove(Vector2 position)
    {
        _rectTrm.anchoredPosition = position;
    }
}
