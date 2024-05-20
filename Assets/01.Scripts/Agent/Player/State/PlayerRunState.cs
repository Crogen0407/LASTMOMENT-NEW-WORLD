using Crogen.AgentFSM;
using Crogen.PowerfulInput;

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
        _playerMovement.OnSpeedDeadEvent += () =>
        {
            _stateMachine.ChangeState(AgentStateEnum.Idle);
        };
        //Managers
        _inputReader = GameManager.Instance.InputReader;
    }

    public override void Enter()
    {
        base.Enter();
        _inputReader.SpeedUpEvent += _playerMovement.HandleSpeedUp;
        _inputReader.SpeedDownEvent += _playerMovement.HandleSpeedDown;
        _inputReader.MoveDirectionEvent += _playerMovement.HandleMoveDirection;
    }

    public override void Exit()
    {
        _inputReader.SpeedUpEvent -= _playerMovement.HandleSpeedUp;
        _inputReader.SpeedDownEvent -= _playerMovement.HandleSpeedDown;
        _inputReader.MoveDirectionEvent -= _playerMovement.HandleMoveDirection;
        base.Exit();
    }
}
