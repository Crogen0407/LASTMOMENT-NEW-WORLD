using Crogen.AgentFSM;

public class EnemyDeadState : AgentState<AgentStateEnum>
{
    public EnemyDeadState(Agent<AgentStateEnum> agentBase, StateMachine<AgentStateEnum> stateMachine, string animBoolName) : base(agentBase, stateMachine, animBoolName)
    {
    }
}
