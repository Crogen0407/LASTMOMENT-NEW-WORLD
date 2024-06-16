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
        int range = Random.Range(0, 2);

        switch (range)
        {
            case 0:
                _agentBase.StartCoroutine(CoroutineBulletAttack());
                break;
            default:
                (_agentBase as Boss)?.bossAttack.ShootLaser(10, ()=>_stateMachine.ChangeState(BossStateEnum.SpawnEnemy));                
                break;
        }
    }
    
    private IEnumerator CoroutineBulletAttack()
    {
        for (int i = 0; i < 15; ++i)
        {
            yield return new WaitForSeconds(1f);
            (_agentBase as Boss)?.bossAttack.ShootBullet();
        }

        yield return new WaitForSeconds(5f);
        _stateMachine.ChangeState(BossStateEnum.SpawnEnemy);
    }
}
