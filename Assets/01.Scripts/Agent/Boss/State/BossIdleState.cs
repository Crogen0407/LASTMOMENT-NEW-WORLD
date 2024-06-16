using Crogen.AgentFSM;
using UnityEngine;

public class BossIdleState : AgentState<BossStateEnum>
{
    private float _attackRange = 100f;
    private Collider[] _colliders;
    
    public BossIdleState(Agent<BossStateEnum> agentBase, StateMachine<BossStateEnum> stateMachine, string animBoolName) : base(agentBase, stateMachine, animBoolName)
    {
        _colliders = new Collider[1];
        _attackRange = ((Boss)agentBase).recognitionRange;
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();

        if (Physics.OverlapSphereNonAlloc(_agentBase.transform.position, _attackRange, _colliders, ((Boss)_agentBase).whatIsPlayer) > 0)
        {
            _stateMachine.ChangeState(BossStateEnum.Attack);
        }
    }
}
