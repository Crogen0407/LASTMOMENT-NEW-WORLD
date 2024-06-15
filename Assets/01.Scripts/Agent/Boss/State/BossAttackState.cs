using Crogen.AgentFSM;

public class BossAttackState : AgentState<BossStateEnum>
{
    public BossAttackState(Agent<BossStateEnum> agentBase, StateMachine<BossStateEnum> stateMachine, string animBoolName) : base(agentBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        (_agentBase as Boss).bossAttack.currentPattern.EnterPattern();
    }

    public override void Exit()
    {
        (_agentBase as Boss).bossAttack.currentPattern.ExitPattern();
        base.Exit();
    }
}
