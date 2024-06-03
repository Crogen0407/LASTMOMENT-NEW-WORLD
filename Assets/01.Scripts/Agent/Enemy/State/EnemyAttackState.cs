using System.Collections;
using Crogen.AgentFSM;
using Crogen.ObjectPooling;
using UnityEngine;

public class EnemyAttackState : AgentState<EnemyStateEnum>
{
    //Managers
    private GameManager _gameManager;
    //Components
    private Enemy _enemyBase;
    private EnemyMovement _enemyMovement;
    private bool _isAttacking = false;
    private bool _isDelay = false;
    private int _attackCurCount = 10;
    private float _currentDelayTime = 0;
    private bool _endAttack = false;
    public EnemyAttackState(Agent<EnemyStateEnum> agentBase, StateMachine<EnemyStateEnum> stateMachine, string animBoolName) : base(agentBase, stateMachine, animBoolName)
    {
        _enemyBase = agentBase as Enemy;
        _enemyMovement = agentBase.Movement as EnemyMovement;
        
        //Managers
        _gameManager = GameManager.Instance;
    }

    public override void Enter()
    {
        base.Enter();
        _endAttack = false;
        _attackCurCount = _enemyBase.attackMaxCount;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        OnAttack();
    }


    public void OnAttack()
    {
        _currentDelayTime += Time.deltaTime;
        if (_currentDelayTime > _enemyBase.attackDelay)
        {
            _isDelay = false;
        }
        if(_isAttacking || _isDelay || _endAttack) return;
        if (_attackCurCount <= 0)
        {
            _endAttack = true;
            _agentBase.StartDelayCallback(_enemyBase.attackLoadingDelay, () => _stateMachine.ChangeState(EnemyStateEnum.Turning));
        }
        else
        {
            --_attackCurCount;
            _enemyBase.Pop(_enemyBase.bulletType, _enemyBase.transform.position, _enemyBase.transform.rotation);
        }
    }

    
}
