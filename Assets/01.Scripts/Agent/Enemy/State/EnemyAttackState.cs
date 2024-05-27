using Crogen.AgentFSM;
using UnityEngine;

public class EnemyAttackState : EnemyRunState
{
    public EnemyAttackState(Agent<EnemyStateEnum> agentBase, StateMachine<EnemyStateEnum> stateMachine, string animBoolName) : base(agentBase, stateMachine, animBoolName)
    {
    }

    public override void UpdateState()
    {
        base.UpdateState();
        Vector3 delta = ((_enemyBase.currentTarget.position - _enemyBase.transform.position));
        _enemyMovement.HandleMoveDirection(delta);
    }
}