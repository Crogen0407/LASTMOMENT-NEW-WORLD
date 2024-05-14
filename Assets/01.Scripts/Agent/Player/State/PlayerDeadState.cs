using Crogen.AgentFSM;

public class PlayerDeadState : AgentState<AgentStateEnum>
{
    public PlayerDeadState(Agent<AgentStateEnum> agentBase, StateMachine<AgentStateEnum> stateMachine, string animBoolName) : base(agentBase, stateMachine, animBoolName)
    {
    }
}
