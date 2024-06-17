using Crogen.AgentFSM;

public class BossSpawnEnemyState : AgentState<BossStateEnum>
{
    public BossSpawnEnemyState(Agent<BossStateEnum> agentBase, StateMachine<BossStateEnum> stateMachine, string animBoolName) : base(agentBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        ((Boss)_agentBase).enemySpawner.SpawnEnemy(0.8f, 10f, () => _stateMachine.ChangeState(BossStateEnum.Idle));
    }
}