using Crogen.AgentFSM;
using UnityEngine;

public class EnemyIdleState : AgentState<AgentStateEnum>
{
    private Enemy _enemyBase;
    
    public EnemyIdleState(Agent<AgentStateEnum> agentBase, StateMachine<AgentStateEnum> stateMachine, string animBoolName) : base(agentBase, stateMachine, animBoolName)
    {
        _enemyBase = agentBase as Enemy;
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
