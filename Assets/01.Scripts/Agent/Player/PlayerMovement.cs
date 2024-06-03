using System;
using DG.Tweening;
using UnityEngine;

public class PlayerMovement : AgentMovement
{
    //Managements
    private GameManager _gameManager;
    private UIManager _uiManager;

    //Components
    private AgentEffectGenerator _agentEffectGenerator;
    
    
    [field:SerializeField] public float RotateSpeedX { get; set; }
    [field:SerializeField] public float RotateSpeedY { get; set; }
    public float aimingDistance;
    [HideInInspector] public Vector3 lookAngle = Vector3.zero;
    private bool _isResettingDirection = false;
    
    protected override void Awake()
    {
        base.Awake();
        
        //Managements
        _gameManager = GameManager.Instance;
        _uiManager = UIManager.Instance;
        
        //Components
        _agentEffectGenerator = GetComponent<AgentEffectGenerator>();
        
        _gameManager.InputReader.ChangeMoveDirectionEvent += HandleMoveDirection;
        _gameManager.InputReader.ResetDirectionEvent += ResetDirection;
    }

    private void OnDestroy()
    {
        _gameManager.InputReader.ChangeMoveDirectionEvent -= HandleMoveDirection;
        _gameManager.InputReader.ResetDirectionEvent -= ResetDirection;    
    }

    public override void HandleMoveDirection(Vector3 position)
    {
        Vector3 rotate = transform.rotation * new Vector3(
            -position.y * RotateSpeedY,
            position.x * RotateSpeedX,
            0);
        transform.Rotate(rotate*Time.deltaTime, Space.World);
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