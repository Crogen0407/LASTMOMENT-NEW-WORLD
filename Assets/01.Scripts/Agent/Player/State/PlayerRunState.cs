using Crogen.AgentFSM;
using Crogen.PowerfulInput;
using UnityEngine;

public class PlayerRunState : AgentState<AgentStateEnum>
{
    //Managers
    private GameManager _gameManager;
    
    //Controllers
    
    //Components
    private Rigidbody _rigidbody;
    
    //Transforms
    private Transform _playerTrm;

    //Value
    private InputReader _inputReader;
    private Player _playerBase;
    
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
        
        _inputReader = _gameManager.InputReader;

    }

    public override void Enter()
    {
        base.Enter();
        _inputReader.ChangeScrollEvent += HandleSpeedChange;
        _inputReader.MoveDirectionEvent += HandleMoveDirection;
    }

    public override void Exit()
    {
        base.Exit();
        _inputReader.ChangeScrollEvent -= HandleSpeedChange;
        _inputReader.MoveDirectionEvent -= HandleMoveDirection;
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
        _rigidbody.velocity = _playerTrm.forward * _playerBase.CurSpeed;
    }
    
    private void HandleSpeedChange(float axis)
    {
        int speedValue = (int)(_playerBase.MaxSpeed * ((axis / 24) * 0.01f)); 
        
        _playerBase.CurSpeed += speedValue;

        _playerBase.CurSpeed = Mathf.Clamp(_playerBase.CurSpeed, 0, _playerBase.MaxSpeed);
        if (_playerBase.CurSpeed <= 0)
        {
            _stateMachine.ChangeState(AgentStateEnum.Idle);
        }
    }

    private void HandleMoveDirection(Vector2 mouseDelta)
    {
        _playerBase.lookAngle += new Vector3(
            -mouseDelta.y * _playerBase.RotateSpeedY, 
            mouseDelta.x * _playerBase.RotateSpeedX * 0.5f, 
            0) * Time.deltaTime * ((float)_playerBase.CurSpeed/_playerBase.MaxSpeed*0.5f);
        _playerBase.lookAngle = new Vector3(MathExtension.RotateClamp(_playerBase.lookAngle.x, -90f, 90f), _playerBase.lookAngle.y, _playerBase.lookAngle.z);
        _playerTrm.eulerAngles = _playerBase.lookAngle;
    }
}
