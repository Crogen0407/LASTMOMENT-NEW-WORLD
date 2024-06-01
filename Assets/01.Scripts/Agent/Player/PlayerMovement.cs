using DG.Tweening;
using UnityEngine;

public class PlayerMovement : AgentMovement
{
    //Managements
    private GameManager _gameManager;
    private UIManager _uiManager;

    //Components
    private AgentEffectGenerator _agentEffectGenerator;
    
    public float aimingDistance;
    [HideInInspector] public Vector3 lookAngle = Vector3.zero;
    private bool _isResettingDirection = false;
    
    protected override void Awake()
    {
        base.Awake();
        Debug.Log(MathExtension.Remap(0.5f, 0, 1, 0, 12));
        //Managements
        _gameManager = GameManager.Instance;
        _uiManager = UIManager.Instance;
        
        //Components
        _agentEffectGenerator = GetComponent<AgentEffectGenerator>();
        
        _gameManager.InputReader.ChangeMoveDirectionEvent += StartMoveDirection;
        _gameManager.InputReader.ResetDirectionEvent += ResetDirection;
    }

    private void StartMoveDirection(Vector3 vec, bool active)
    {
        if (active)
        {
            HandleMoveDirection(vec);
        }
    }
    
    public override void HandleMoveDirection(Vector3 position)
    {
        Debug.Log(_uiManager.ScreenConvertToCanvasSpace(position));
        
        Quaternion quaternion = Quaternion.Euler(new Vector3(position.y*0.1f, position.x*0.1f, 0) * Time.deltaTime);
        transform.rotation *= Quaternion.Inverse(transform.rotation) * quaternion * transform.rotation;
        
    }

    public override void HandleSpeedChange(bool value)
    {
        base.HandleSpeedChange(value);
        _agentEffectGenerator.SetHighBusterEffect(value);
    }

    private void ResetDirection()
    {
        if (_isResettingDirection) return;
        _isResettingDirection = true;
        transform.DORotate(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0), 1f).OnComplete(() =>
        {
            _isResettingDirection = false;
        });
    }
}