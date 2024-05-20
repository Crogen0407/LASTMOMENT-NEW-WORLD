using Crogen.AgentFSM;
using UnityEngine;

public class PlayerRunState : AgentState<AgentStateEnum>
{
    //Managers
    private GameManager _gameManager;
    
    //Components
    private Rigidbody _rigidbody;
    
    //Transforms
    private Transform _playerTrm;

    //Value
    private Player _playerBase;
    private bool _isSpeedUp = false;
    private float _holdTime = 0f;
    
    public PlayerRunState(Agent<AgentStateEnum> agentBase, StateMachine<AgentStateEnum> stateMachine, string animBoolName) : base(agentBase, stateMachine, animBoolName)
    {
        _playerBase = agentBase as Player;

        //Components
        _rigidbody = _playerBase.rigidbodyCompo;
        
        //Controllers
        
        //Managers
        _gameManager = GameManager.Instance;
        
        //Transforms
        _playerTrm = _playerBase.transform;
    }

    public override void Enter()
    {
        base.Enter();
        _gameManager.InputReader.SpeedUpEvent += HandleSpeedUp;
        _gameManager.InputReader.SpeedDownEvent += HandleSpeedDown;
        _gameManager.InputReader.MoveDirectionEvent += HandleMoveDirection;
    }

    public override void Exit()
    {
        base.Exit();
        _gameManager.InputReader.SpeedUpEvent -= HandleSpeedUp;
        _gameManager.InputReader.SpeedDownEvent -= HandleSpeedDown;
        _gameManager.InputReader.MoveDirectionEvent -= HandleMoveDirection;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        OnSpeedChange(_isSpeedUp);
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
        _rigidbody.velocity = _playerTrm.forward * _playerBase.CurSpeed;
    }

    #region Speed Control

    private void HandleSpeedUp()
    {
        _isSpeedUp = true;
    }
    private void HandleSpeedDown()
    {
        _isSpeedUp = false;
    }
    private void OnSpeedChange(bool isSpeedUp)
    {
        if (isSpeedUp)
        {
            if (_playerBase.CurSpeed < _playerBase.MaxSpeed)
                _holdTime += Time.deltaTime;
            _playerBase.CurSpeed = (int)(EaseInCubic(_holdTime) * _playerBase.MaxSpeed);

            _playerBase.CurSpeed = Mathf.Clamp(_playerBase.CurSpeed, 0, _playerBase.MaxSpeed);
        }
        else
        {
            _holdTime -= Time.deltaTime;
            _playerBase.CurSpeed = (int)(EaseInDefault(_holdTime) * _playerBase.MaxSpeed);
            _playerBase.CurSpeed = Mathf.Clamp(_playerBase.CurSpeed, 0, _playerBase.MaxSpeed);
            if (_playerBase.CurSpeed <= 0)
            {
                _stateMachine.ChangeState(AgentStateEnum.Idle);
            }
        }
    }
    
    #endregion

    private void HandleMoveDirection(Vector2 mouseDelta)
    {
        _playerBase.lookAngle += new Vector3(
            -mouseDelta.y * _playerBase.RotateSpeedY, 
            mouseDelta.x * _playerBase.RotateSpeedX * 0.5f, 
            0) * Time.deltaTime * ((float)_playerBase.CurSpeed/_playerBase.MaxSpeed*0.5f);
        _playerBase.lookAngle = new Vector3(MathExtension.RotateClamp(_playerBase.lookAngle.x, -90f, 90f), _playerBase.lookAngle.y, _playerBase.lookAngle.z);
        _playerTrm.eulerAngles = _playerBase.lookAngle;
    }
    
    private float EaseInCubic(float x) 
    {
        return x * x * x;
    }
    private float EaseInDefault(float x) 
    {
        return x;
    }
}
