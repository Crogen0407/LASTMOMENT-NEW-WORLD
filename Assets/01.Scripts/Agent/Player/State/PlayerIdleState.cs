using Crogen.AgentFSM;
using UnityEngine;

public class PlayerIdleState : AgentState<AgentStateEnum>
{
    private Player _playerBase;
    
    //Managers
    private GameManager _gameManager;
    
    //Components
    private PlayerMovement _playerMovement;
    
    public PlayerIdleState(Agent<AgentStateEnum> agentBase, StateMachine<AgentStateEnum> stateMachine, string animBoolName) : base(agentBase, stateMachine, animBoolName)
    {
        _gameManager = GameManager.Instance;
        
        _playerBase = agentBase as Player;
        
        //Components
        _playerMovement = _playerBase.Movement as PlayerMovement;
    }

    public override void Enter()
    {
        base.Enter();
        _gameManager.InputReader.StartRunEvent += HandleStartMove;
    }

    public override void Exit()
    {
        _gameManager.InputReader.StartRunEvent -= HandleStartMove;       
        base.Exit();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        _playerBase.Stamina += Time.deltaTime;
    }

    private void HandleStartMove()
    {
        if(_playerBase.Stamina > _playerBase.maxStamina * 0.5f)
            _stateMachine.ChangeState(AgentStateEnum.Run);
    }
}
