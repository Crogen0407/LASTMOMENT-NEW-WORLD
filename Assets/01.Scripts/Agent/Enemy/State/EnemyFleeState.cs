using Crogen.AgentFSM;

public class EnemyFleeState : EnemyRunState
{
    public EnemyFleeState(Agent<EnemyStateEnum> agentBase, StateMachine<EnemyStateEnum> stateMachine, string animBoolName) : base(agentBase, stateMachine, animBoolName)
    {
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
        // 뒤를 제외한 방향에 총 13개의 ray를 쏴서 도망갈 수 있는 방향을 구하고
        // 도망갈 수 있는 방향 중 가장 플레이어랑 가까운 방향으로 이동
    }
}