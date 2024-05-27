using Crogen.AgentFSM;
using UnityEngine;

public class EnemyIdleState : AgentState<EnemyStateEnum>
{
    private Enemy _enemyBase;

    public EnemyIdleState(Agent<EnemyStateEnum> agentBase, StateMachine<EnemyStateEnum> stateMachine, string animBoolName) : base(agentBase, stateMachine, animBoolName)
    {
        _enemyBase = agentBase as Enemy;
    }

    public override void Enter()
    {
        base.Enter();
        (_enemyBase.Movement as EnemyMovement)?.EnterDefaultBezierPath();
    }

    public override void Exit()
    {
        base.Exit();
        (_enemyBase.Movement as EnemyMovement)?.ExitDefaultBezierPath();
    }

    
    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
        Collider[] playerCol = new Collider[1];
        Physics.OverlapSphereNonAlloc(_agentBase.transform.position, _enemyBase.recognitionRange, playerCol, _enemyBase.whatIsPlayer);

        if (playerCol[0] != null)
        {
            Transform playerTrm = playerCol[0].transform;
            //앞 뒤 판별 (플레이어를 발견하면 도망갈지 공격할지 정함)
            //dotValue가 음수(-)면 앞, 양수(+)면 뒤
            float dotValue = Vector3.Dot(playerTrm.position, _enemyBase.transform.position);
            if (dotValue < 0) //앞
            {
                _stateMachine.ChangeState(EnemyStateEnum.Flee);
            }
            else //뒤
            {
                _enemyBase.currentTarget = playerTrm;
                _stateMachine.ChangeState(EnemyStateEnum.Attack);
            }
        }
    }
}
