using Crogen.AgentFSM;
using UnityEngine;

public class EnemyIdleState : AgentState<EnemyStateEnum>
{
    //Managers
    private GameManager _gameManager;
    
    //Components
    private Enemy _enemyBase;
    private EnemyMovement _enemyMovement;

    public EnemyIdleState(Agent<EnemyStateEnum> agentBase, StateMachine<EnemyStateEnum> stateMachine, string animBoolName) : base(agentBase, stateMachine, animBoolName)
    {
        _enemyBase = agentBase as Enemy;
        _enemyMovement = agentBase.Movement as EnemyMovement;
        
        //Managers
        _gameManager = GameManager.Instance;
    }

    public override void Enter()
    {
        base.Enter();
        _enemyMovement.EnterDefaultBezierPath();
    }

    public override void Exit()
    {
        base.Exit();
        _enemyMovement.ExitDefaultBezierPath();
    }

    
    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
        Collider[] playerCol = new Collider[1];
        Physics.OverlapSphereNonAlloc(_agentBase.transform.position, _enemyBase.recognitionRange, playerCol, _enemyBase.whatIsPlayer);

        if (playerCol[0] != null)
        {
            _enemyMovement.attackTargetTrm = playerCol[0].transform;
            _stateMachine.ChangeState(EnemyStateEnum.Run);
        }
    }
}
