using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TargetPointContent : MonoPoolingObject
{
    public Transform targetTrasform;
    private Color _imageColor;
    public Color ImageColor
    {
        set
        {
            _imageColor = value;
            _image.color = _imageColor;
        }
        get => _imageColor;
	}

    private Transform _playerCameraTrm;
    private Transform _playerTrm;    
    
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _distanceText;
    
    private void Awake()
    {
        _playerCameraTrm = GameObject.Find("PlayerVirtualCamera").transform;
        _playerTrm = GameManager.Instance.Player.transform;
    }

    private void FixedUpdate()
    {
        _image.enabled = targetTrasform != null;
        if (targetTrasform == null) return;
        float minX = _image.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;
        
        float minY = _image.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.height - minY;
        
        Vector2 pos = Camera.main.WorldToScreenPoint(targetTrasform.position);

        if (Vector3.Dot((targetTrasform.position - _playerCameraTrm.position),  _playerCameraTrm.forward) < 0)
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
        pos.y = Mathf.Clamp(pos.y, minY + 37.4f, maxY);

        _distanceText.text = $"{(int)Vector3.Distance(targetTrasform.position, _playerTrm.position)}m";
        
        _image.transform.position = pos;
    }

	public override void OnPop()
	{
	}

	public override void OnPush()
	{
	}
}
