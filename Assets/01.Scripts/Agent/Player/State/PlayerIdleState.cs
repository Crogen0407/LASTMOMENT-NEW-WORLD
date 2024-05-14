using Crogen.AgentFSM;
using Crogen.PowerfulInput;
using UnityEngine;

public class PlayerIdleState : AgentState<AgentStateEnum>
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
    
    public PlayerIdleState(Agent<AgentStateEnum> agentBase, StateMachine<AgentStateEnum> stateMachine, string animBoolName) : base(agentBase, stateMachine, animBoolName)
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
    }

    public override void Exit()
    {
        base.Exit();
        _inputReader.ChangeScrollEvent -= HandleSpeedChange;
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }
    
    private void HandleSpeedChange(float axis)
    {
        int speedValue = (int)(_playerBase.MaxSpeed * ((axis / 24) * 0.01f)); 
        
        _playerBase.CurSpeed += speedValue;

        _playerBase.CurSpeed = Mathf.Clamp(_playerBase.CurSpeed, 0, _playerBase.MaxSpeed);
        
        if (_playerBase.CurSpeed > 0)
        {
            _stateMachine.ChangeState(AgentStateEnum.Run);
        }
    }
}
