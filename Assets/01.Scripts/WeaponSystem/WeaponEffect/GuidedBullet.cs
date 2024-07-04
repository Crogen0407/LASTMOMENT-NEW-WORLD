using Crogen.HealthSystem;
using Crogen.ObjectPooling;
using DG.Tweening;
using UnityEngine;

public class GuidedBullet : WeaponEffect
{
    private Collider _attackTarget;
    [SerializeField] private PoolType _explosionEffectPoolType;
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _damaged = 10f;
    
    public override void Init(Vector3 attackDirection, Transform parent = null)
    {
        base.Init(attackDirection, null);
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOLocalMoveY(transform.position.y-1, 1));

        _attackTarget = _attackTargets[0];
        for (int i = 1; i < _attackTargets.Length; ++i)
        {
            if (Vector3.Distance(_attackTarget.transform.position, transform.position) >
                Vector3.Distance(_attackTargets[i].transform.position, transform.position))
            {
                _attackTarget = _attackTargets[i];
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);

        if (other.transform.TryGetComponent(out HealthSystem healthSystem))
        {
            healthSystem.Hp -= _damaged;
        }
        else if (other.transform.transform.parent.TryGetComponent(out HealthSystem healthSystemInParent))
        {
            healthSystemInParent.Hp -= _damaged;
        }
    }

    private void FixedUpdate()
    {
        Physics.OverlapSphereNonAlloc(transform.position, attackRange, _attackTargets, _whatIsEnemy);

        if (_attackTarget != null)
        {
            transform.DOMove(_attackTarget.transform.position, duration / _speed);
            transform.forward = (_attackTarget.transform.position - transform.position).normalized;
        }
        else
        {
            transform.DOMove(transform.position + (transform.forward * _speed), duration);
        }
        if (duration < _curLifeTime)
        {
            Destroy(gameObject);
        }
        else
        {
            _curLifeTime += Time.deltaTime;
        }
    }

    private void OnDestroy()
    {
        this.Pop(_explosionEffectPoolType, transform.position, Quaternion.identity);
        transform.DOKill();
    }
    
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * _speed);
        Gizmos.color = Color.white;
    }
}
