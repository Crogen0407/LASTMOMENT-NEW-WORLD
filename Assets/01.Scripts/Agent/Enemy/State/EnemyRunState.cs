using Crogen.AgentFSM;
using UnityEngine;

public class EnemyRunState : AgentState<AgentStateEnum>
{
    //Managers
    private GameManager _gameManager;
    
    //Components
    private EnemyMovement _enemyMovement;
    
    private Enemy _enemyBase;
    
    public EnemyRunState(Agent<AgentStateEnum> agentBase, StateMachine<AgentStateEnum> stateMachine, string animBoolName) : base(agentBase, stateMachine, animBoolName)
    {
        _enemyBase = agentBase as Enemy;

        //Components
        _enemyMovement = _enemyBase.Movement as EnemyMovement;
        _enemyMovement.OnSpeedDeadEvent += () =>
        {
            _stateMachine.ChangeState(AgentStateEnum.Idle);
        };
        //Managers
        _gameManager = GameManager.Instance;
    }
    
    public override void Enter()
    {
        base.Enter();
        _enemyMovement.HandleSpeedUp();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        Vector3 delta = ((_enemyBase.currentTarget.position - _enemyBase.transform.position));
        _enemyMovement.HandleMoveDirection(delta);
    }
}
