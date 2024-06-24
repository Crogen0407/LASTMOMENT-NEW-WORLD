using Crogen.AgentFSM;
using DG.Tweening;
using UnityEngine;

public class EnemyTurningState : AgentState<EnemyStateEnum>
{
    //Managers
    private GameManager _gameManager;
    
    //Components
    private Enemy _enemyBase;
    private EnemyMovement _enemyMovement;
    
    public EnemyTurningState(Agent<EnemyStateEnum> agentBase, StateMachine<EnemyStateEnum> stateMachine, string animBoolName) : base(agentBase, stateMachine, animBoolName)
    {
        _enemyBase = agentBase as Enemy;
        _enemyMovement = agentBase.Movement as EnemyMovement;
        
        //Managers
        _gameManager = GameManager.Instance;
    }
    
    public override void Enter()
    {
        base.Enter();
        Vector3 dir = _enemyMovement.attackTargetTrm.position - _enemyMovement.transform.position;
        _enemyBase.transform.DORotateQuaternion(Quaternion.LookRotation(dir), 
            _enemyMovement.rotateDelay).OnComplete(()=>_stateMachine.ChangeState(EnemyStateEnum.Run));
    }

    public override void Exit()
    {
        base.Exit();
    }
}
