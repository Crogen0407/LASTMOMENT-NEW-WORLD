using Crogen.AgentFSM;
using Crogen.PowerfulInput;
using UnityEngine;

public class PlayerIdleState : AgentState<AgentStateEnum>
{
    //Managers
    private GameManager _gameManager;
    
    public PlayerIdleState(Agent<AgentStateEnum> agentBase, StateMachine<AgentStateEnum> stateMachine, string animBoolName) : base(agentBase, stateMachine, animBoolName)
    {
        _gameManager = GameManager.Instance;
    }

    public override void Enter()
    {
        base.Enter();
        _gameManager.InputReader.StartRunEvent += HandleStartMove;
    }

    public override void Exit()
    {
        base.Exit();
        _gameManager.InputReader.StartRunEvent -= HandleStartMove;
    }
    
    private void HandleStartMove()
    {
        _stateMachine.ChangeState(AgentStateEnum.Run);
    }
}
