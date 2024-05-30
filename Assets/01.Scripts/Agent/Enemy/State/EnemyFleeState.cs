using Crogen.AgentFSM;
using UnityEngine;

public class EnemyFleeState : EnemyRunState
{
    
    public EnemyFleeState(Agent<EnemyStateEnum> agentBase, StateMachine<EnemyStateEnum> stateMachine, string animBoolName) : base(agentBase, stateMachine, animBoolName)
    {
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
    }
}