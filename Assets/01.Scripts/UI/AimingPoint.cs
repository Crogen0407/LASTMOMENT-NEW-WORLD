using UnityEngine;

public class AimingPoint : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    private RectTransform _rectTrm;
    
    private readonly float _width = Screen.width;
    private readonly float _height = Screen.height;
    
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
        Vector2 canvasRectSize = ((RectTransform)_canvas.transform).rect.size;
        float x = MathExtension.Remap(position.x, 0, _width, -canvasRectSize.x * 0.5f, canvasRectSize.x * 0.5f);
        float y = MathExtension.Remap(position.y, 0, _height, -canvasRectSize.y * 0.5f, canvasRectSize.y * 0.5f);

        _rectTrm.anchoredPosition = new Vector2(x, y);
    }
}
