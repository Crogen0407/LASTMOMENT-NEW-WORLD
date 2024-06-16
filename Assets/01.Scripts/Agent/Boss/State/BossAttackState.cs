using System.Collections;
using Crogen.AgentFSM;
using UnityEngine;

public class BossAttackState : AgentState<BossStateEnum>
{
    public BossAttackState(Agent<BossStateEnum> agentBase, StateMachine<BossStateEnum> stateMachine, string animBoolName) : base(agentBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        (_agentBase as Boss)?.bossAttack.currentPattern.EnterPattern();
        _agentBase.StartCoroutine(CoroutineAttack());
    }

    public override void Exit()
    {
        (_agentBase as Boss)?.bossAttack.currentPattern.ExitPattern();
        base.Exit();
    }

    private IEnumerator CoroutineAttack()
    {
        for (int i = 0; i < 15; ++i)
        {
            yield return new WaitForSeconds(1f);
            (_agentBase as Boss)?.bossAttack.Attack();
        }

        yield return new WaitForSeconds(5f);
        _stateMachine.ChangeState(BossStateEnum.SpawnEnemy);
    }
}
