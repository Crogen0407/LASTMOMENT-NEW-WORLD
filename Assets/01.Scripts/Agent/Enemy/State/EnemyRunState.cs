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
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
        
        //회전
        Vector3 dir = _enemyMovement.attackTargetTrm.position - _enemyMovement.transform.position;
        _enemyMovement.HandleMoveDirection(dir);

        //이동은 EnemyMovement에서 한다.

        //공격
        if(Physics.BoxCast(_enemyBase.transform.position, new Vector3(5, 5, _enemyBase.recognitionRange*3),
               _enemyBase.transform.forward, _enemyBase.transform.rotation, _enemyBase.whatIsPlayer))
        {
            _stateMachine.ChangeState(EnemyStateEnum.Turning);
        }
    }
}
