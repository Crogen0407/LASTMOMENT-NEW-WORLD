using Crogen.AgentFSM;

public class BossBulletAttackState : AgentState<BossStateEnum>
{
    public BossBulletAttackState(Agent<BossStateEnum> agentBase, StateMachine<BossStateEnum> stateMachine, string animBoolName) : base(agentBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        ((Boss)_agentBase).bossAttack.ShootBullet(0.5f, 15f, () => _stateMachine.ChangeState(BossStateEnum.Idle));
    }
}
