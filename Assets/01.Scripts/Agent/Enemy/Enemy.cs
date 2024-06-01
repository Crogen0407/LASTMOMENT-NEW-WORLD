using System.Collections;
using Crogen.AgentFSM;
using Crogen.ObjectPooling;
using UnityEngine;

public enum Direction
{
    right,
    left,
    up, 
    down
}

public class Enemy : Agent<EnemyStateEnum>
{
    public LayerMask whatIsPlayer;
    public float recognitionRange = 50f;
    public Transform currentTarget;
    public Direction preferredDirection;
    [SerializeField]private int findNavAngle;

    [Header("Attack")] 
    [SerializeField] private PoolType _bulletType;
    [SerializeField] private float _attackDelay = 0.1f;
    [SerializeField] private int _attackMaxCount = 10;
    [SerializeField] private int _attackCurCount = 10;
    [SerializeField] private float _attackLoadingDelay = 5f;
    private bool _isAttacking = false;
    
    public override void SetDead()
    {
        
    }

    public void OnAttack()
    {
        if(_isAttacking == true) return;
        StartCoroutine(OnAttackCoroutine());
    }

    private IEnumerator OnAttackCoroutine()
    {
        _isAttacking = true;
        while (_attackCurCount > 0)
        {
            this.Pop(_bulletType, transform.position, transform.rotation);
            --_attackCurCount;
            yield return new WaitForSeconds(_attackDelay);
        }
        yield return StartCoroutine(BulletLoadingCoroutine());
        _isAttacking = false;
    }

    private IEnumerator BulletLoadingCoroutine()
    {
        yield return new WaitForSeconds(_attackLoadingDelay);
        _attackCurCount = _attackMaxCount;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, recognitionRange);
    }
}