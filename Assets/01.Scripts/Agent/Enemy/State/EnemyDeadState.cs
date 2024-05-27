using Crogen.AgentFSM;

public class EnemyDeadState : AgentState<EnemyStateEnum>
{
    public EnemyDeadState(Agent<EnemyStateEnum> agentBase, StateMachine<EnemyStateEnum> stateMachine, string animBoolName) : base(agentBase, stateMachine, animBoolName)
    {
    }
}
