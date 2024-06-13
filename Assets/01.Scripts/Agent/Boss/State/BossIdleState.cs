using Crogen.AgentFSM;

public class BossIdleState : AgentState<BossStateEnum>
{
    public BossIdleState(Agent<BossStateEnum> agentBase, StateMachine<BossStateEnum> stateMachine, string animBoolName) : base(agentBase, stateMachine, animBoolName)
    {
    }
}
