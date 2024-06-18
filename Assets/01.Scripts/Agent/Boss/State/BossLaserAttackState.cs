using System.Collections;
using Crogen.AgentFSM;
using UnityEngine;

public class BossLaserAttackState : AgentState<BossStateEnum>
{
    public BossLaserAttackState(Agent<BossStateEnum> agentBase, StateMachine<BossStateEnum> stateMachine, string animBoolName) : base(agentBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        (_agentBase as Boss)?.bossAttack.ShootLaser(10, ()=>_stateMachine.ChangeState(BossStateEnum.Idle));                
    }
}
