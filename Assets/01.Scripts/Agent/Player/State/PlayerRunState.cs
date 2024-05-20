using Crogen.AgentFSM;

public class PlayerRunState : AgentState<AgentStateEnum>
{
    //Managers
    private GameManager _gameManager;
    
    //Components
    private PlayerMovement _playerMovement;
    
    //Value
    private Player _playerBase;
    
    
    public PlayerRunState(Agent<AgentStateEnum> agentBase, StateMachine<AgentStateEnum> stateMachine, string animBoolName) : base(agentBase, stateMachine, animBoolName)
    {
        _playerBase = agentBase as Player;

        //Components
        _playerMovement = _playerBase.Movement as PlayerMovement;
        
        //Managers
        _gameManager = GameManager.Instance;
    }

    public override void Enter()
    {
        base.Enter();
        _gameManager.InputReader.SpeedUpEvent += _playerMovement.HandleSpeedUp;
        _gameManager.InputReader.SpeedDownEvent += _playerMovement.HandleSpeedDown;
        _gameManager.InputReader.MoveDirectionEvent += _playerMovement.HandleMoveDirection;
    }

    public override void Exit()
    {
        base.Exit();
        _gameManager.InputReader.SpeedUpEvent -= _playerMovement.HandleSpeedUp;
        _gameManager.InputReader.SpeedDownEvent -= _playerMovement.HandleSpeedDown;
        _gameManager.InputReader.MoveDirectionEvent -= _playerMovement.HandleMoveDirection;
    }
}
