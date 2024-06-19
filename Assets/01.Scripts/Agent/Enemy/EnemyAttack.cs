using System;
using UnityEngine;

public class EnemyAttack : AgentAttack
{
    //Managers
    private GameManager _gameManager;
    
    //Components
    private Enemy _enemyBase;
    private EnemyMovement _enemyMovement;
    
    private bool _isAttackReady = false;
    private int _attackCurCount = 10;
    private float _currentDelayTime = 0;
    private bool _endAttack = false;

    protected override void Awake()
    {
        base.Awake();
        _enemyBase = GetComponent<Enemy>();
        _enemyMovement = GetComponent<EnemyMovement>();
    }

    private void FixedUpdate()
    {
        if (_enemyMovement.attackTargetTrm != null)
        {
            Vector3 targetToSelfDirection = (_enemyMovement.attackTargetTrm.position - transform.position).normalized;
            float targetToSelfAngle = Vector3.Angle(targetToSelfDirection, transform.forward);
        
            if(targetToSelfAngle < 30f)
            {
                OnAttack();
            }    
        }
    }

    private void OnValidate()
    {
        _enemyBase = GetComponent<Enemy>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.forward * _enemyBase.recognitionRange);
        Gizmos.color = Color.white;
    }
}
