using System.Collections;
using Crogen.AgentFSM;
using Crogen.PowerfulInput;
using UnityEngine;

public class PlayerRunState : AgentState<AgentStateEnum>
{
    private InputReader _inputReader;
    
    //Components
    private PlayerMovement _playerMovement;
    
    private Player _playerBase;

    public PlayerRunState(Agent<AgentStateEnum> agentBase, StateMachine<AgentStateEnum> stateMachine, string animBoolName) : base(agentBase, stateMachine, animBoolName)
    {
        _playerBase = agentBase as Player;

        //Components
        _playerMovement = _playerBase.Movement as PlayerMovement;
        _playerMovement.OnStopEvent += () =>
        {
            _stateMachine.ChangeState(AgentStateEnum.Idle);
        };
        //Managers
        _inputReader = GameManager.Instance.InputReader;
    }

    public override void Enter()
    {
        base.Enter();
        _inputReader.SpeedChangeEvent += _playerMovement.HandleSpeedChange;
    }

    public override void Exit()
    {
        _inputReader.SpeedChangeEvent -= _playerMovement.HandleSpeedChange;
        base.Exit();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        _playerBase.Stamina -= Time.deltaTime;
        if (_playerBase.Stamina < 0.1f)
        {
            _playerBase.Movement.HandleSpeedChange(false);
            _stateMachine.ChangeState(AgentStateEnum.Idle);
        }
    }
}
