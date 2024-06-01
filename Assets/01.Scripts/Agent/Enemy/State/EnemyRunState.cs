using Crogen.AgentFSM;
using DG.Tweening;
using UnityEngine;

public class EnemyRunState : AgentState<EnemyStateEnum>
{
    //Managers
    private GameManager _gameManager;
    //Components
    private Enemy _enemyBase;
    private EnemyMovement _enemyMovement;
    
    public EnemyRunState(Agent<EnemyStateEnum> agentBase, StateMachine<EnemyStateEnum> stateMachine, string animBoolName) : base(agentBase, stateMachine, animBoolName)
    {
        _enemyBase = agentBase as Enemy;
        _enemyMovement = agentBase.Movement as EnemyMovement;
        
        //Managers
        _gameManager = GameManager.Instance;
    }
    
    public override void Enter()
    {
        base.Enter();
        _enemyMovement.HandleSpeedChange(true);
        Debug.Log(_enemyMovement.CurSpeed);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (Physics.Raycast(_enemyBase.transform.position,
                _enemyBase.transform.forward,
                _enemyBase.recognitionRange, ~_enemyBase.whatIsPlayer))
        {
            _enemyMovement.transform.eulerAngles += Vector3.up;
        }
    }
    
    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
        //회전
        if (Vector3.Distance(_enemyMovement.transform.position, _enemyMovement.attackTargetTrm.position) > _enemyBase.recognitionRange*3)
        {
            Vector3 dir = _enemyMovement.attackTargetTrm.position - _enemyMovement.transform.position;
            _enemyMovement.HandleMoveDirection(dir);
        }

        //공격
        Debug.DrawRay(_enemyBase.transform.position, _enemyBase.transform.forward * _enemyBase.recognitionRange);
        if(Physics.BoxCast(_enemyBase.transform.position, new Vector3(2, 2, _enemyBase.recognitionRange),
               _enemyBase.transform.forward, _enemyBase.transform.rotation, _enemyBase.whatIsPlayer))
        {
            Debug.Log("Attack");
            _enemyBase.OnAttack();
        }
    }
}
