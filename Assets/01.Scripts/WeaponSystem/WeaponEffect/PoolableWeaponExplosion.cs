using System;
using System.Collections;
using Crogen.HealthSystem;
using UnityEngine;

public class PoolableWeaponExplosion : MonoPoolingObject
{
    [SerializeField] private PoolType _poolType;
    [SerializeField] private float _lifeTime = 1f;
    [SerializeField] private float _attackRange = 5f;
    [SerializeField] private int _attackAbleCount = 1;
    [SerializeField] private float _damage = 1;
    [SerializeField] private LayerMask _whatIsEnemy;
    private float _currentTime = 0;
    protected event Action _dieEvent;
    private Collider[] _colliders;
    
    public override void OnPop()
    {
        _colliders = new Collider[_attackAbleCount];
        Physics.OverlapSphereNonAlloc(transform.position, _attackRange, _colliders, _whatIsEnemy);
        for (int i = 0; i < _colliders.Length; ++i)
        {
            if (_colliders[i].TryGetComponent(out HealthSystem healthSystem))
            {
                healthSystem.Hp -= _damage;
            }
        }
        
        StartCoroutine(CoroutineDie());
    }

    public override void OnPush()
    {
        _dieEvent?.Invoke();
        _currentTime = 0;
    }

    private IEnumerator CoroutineDie()
    {
        while (_lifeTime > _currentTime)
        {
            _currentTime += Time.deltaTime;
            yield return null;
        }
        Push(_poolType);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
        Gizmos.color = Color.white;
    }
}
