using Crogen.AgentFSM;

public class BossSpawnEnemyState : AgentState<BossStateEnum>
{
    public BossSpawnEnemyState(Agent<BossStateEnum> agentBase, StateMachine<BossStateEnum> stateMachine, string animBoolName) : base(agentBase, stateMachine, animBoolName)
    {
    }
}