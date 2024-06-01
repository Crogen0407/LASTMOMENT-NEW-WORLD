using Crogen.AgentFSM;

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
        _playerMovement.OnStopEvent += () =>
        {
            _stateMachine.ChangeState(AgentStateEnum.Idle);
        };
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
