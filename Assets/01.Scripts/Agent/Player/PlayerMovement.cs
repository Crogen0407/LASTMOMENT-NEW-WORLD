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
    private bool _isChangeDirection;
    private Vector2 _lookDirectionAddValue;
    private bool _isResettingDirection = false;
    
    protected override void Awake()
    {
        base.Awake();
        
        //Managements
        _gameManager = GameManager.Instance;
        _uiManager = UIManager.Instance;
        
        //Components
        _agentEffectGenerator = GetComponent<AgentEffectGenerator>();
        
        _gameManager.InputReader.ChangeMoveDirectionEvent += StartMoveDirection;
        _gameManager.InputReader.ResetDirectionEvent += ResetDirection;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (_isChangeDirection)
        {
            HandleMoveDirection(_lookDirectionAddValue);
        }
    }

    private void StartMoveDirection(Vector3 vec, bool active)
    {
        _isChangeDirection = active;
        _lookDirectionAddValue = vec;
    }
    
    public override void HandleMoveDirection(Vector3 position)
    {
        Vector3 rotate = transform.rotation * new Vector3(
            -position.y * RotateSpeedY,
            0,
            -position.x * RotateSpeedX);
        transform.Rotate(rotate*Time.fixedDeltaTime, Space.World);
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