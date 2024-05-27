using Crogen.AgentFSM;

public class EnemyRunState : AgentState<EnemyStateEnum>
{
    //Managers
    protected GameManager _gameManager;
    
    //Components
    protected EnemyMovement _enemyMovement;
    
    protected Enemy _enemyBase;
    
    public EnemyRunState(Agent<EnemyStateEnum> agentBase, StateMachine<EnemyStateEnum> stateMachine, string animBoolName) : base(agentBase, stateMachine, animBoolName)
    {
        _enemyBase = agentBase as Enemy;

        //Components
        _enemyMovement = _enemyBase.Movement as EnemyMovement;
        _enemyMovement.OnSpeedDeadEvent += () =>
        {
            _stateMachine.ChangeState(EnemyStateEnum.Dead);
        };
        //Managers
        _gameManager = GameManager.Instance;
    }
    
    public override void Enter()
    {
        base.Enter();
        _enemyMovement.HandleSpeedChange(true);
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }
}
