using Crogen.AgentFSM;
using UnityEngine;

public class EnemyIdleState : AgentState<AgentStateEnum>
{
    private Enemy _enemyBase;
    private Vector3 _lastPos;

    public EnemyIdleState(Agent<AgentStateEnum> agentBase, StateMachine<AgentStateEnum> stateMachine, string animBoolName) : base(agentBase, stateMachine, animBoolName)
    {
        _enemyBase = agentBase as Enemy;
    }

    public override void Enter()
    {
        base.Enter();
        _lastPos = _enemyBase.transform.position;
        (_enemyBase.Movement as EnemyMovement)?.EnterDefaultBezierPath();
    }

    public override void Exit()
    {
        base.Exit();
        (_enemyBase.Movement as EnemyMovement)?.ExitDefaultBezierPath();
    }

    
    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
        Collider[] playerCol = new Collider[1];
        Physics.OverlapSphereNonAlloc(_agentBase.transform.position, _enemyBase.recognitionRange, playerCol, _enemyBase.whatIsPlayer);

        if (playerCol[0] != null)
        {
            _enemyBase.currentTarget = playerCol[0].transform;
            _stateMachine.ChangeState(AgentStateEnum.Run);
        }
    }
}
