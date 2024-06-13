using Crogen.AgentFSM;

public class BossAttackState : AgentState<BossStateEnum>
{
    public BossAttackState(Agent<BossStateEnum> agentBase, StateMachine<BossStateEnum> stateMachine, string animBoolName) : base(agentBase, stateMachine, animBoolName)
    {
    }
}
