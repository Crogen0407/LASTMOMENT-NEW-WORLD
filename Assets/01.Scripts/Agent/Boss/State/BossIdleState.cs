using System;
using Crogen.AgentFSM;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossIdleState : AgentState<BossStateEnum>
{
    private float _attackRange = 100f;
    private Collider[] _colliders;
    private int _stateCount;
    public BossIdleState(Agent<BossStateEnum> agentBase, StateMachine<BossStateEnum> stateMachine, string animBoolName) : base(agentBase, stateMachine, animBoolName)
    {
        _colliders = new Collider[1];
        _attackRange = ((Boss)agentBase).recognitionRange;
        _stateCount = Enum.GetValues(typeof(BossStateEnum)).Length;
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();

        if (Physics.OverlapSphereNonAlloc(_agentBase.transform.position, _attackRange, _colliders, ((Boss)_agentBase).whatIsPlayer) > 0)
        {
            SetStateByRandom();
        }
    }

    private void SetStateByRandom()
    {
        //_stateMachine.ChangeState(BossStateEnum.LaserAttack);
        _stateMachine.ChangeState((BossStateEnum)Random.Range(1, _stateCount));
    }
}
