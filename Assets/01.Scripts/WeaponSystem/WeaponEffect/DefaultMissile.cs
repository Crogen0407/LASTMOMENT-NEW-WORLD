using Crogen.HealthSystem;
using Crogen.ObjectPooling;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultMissile : WeaponEffect
{
    [SerializeField] private PoolType _explosionEffectPoolType;
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _damaged = 10f;
    private Vector3 _targetPos;

    public override void Init(Vector3 attackDirection, Transform parent = null)
    {
        base.Init(attackDirection, null);
        transform.DOLocalMoveY(transform.position.y - 1, 1);
        SoundManager.Instance.PlaySFX(_fireAudioType, transform.position);
        _targetPos = transform.position + transform.forward * duration * _speed;
    }

    private void FixedUpdate()
    {
        Physics.OverlapSphereNonAlloc(transform.position, attackRange, _attackTargets, _whatIsEnemy);

        transform.DOMove(_targetPos, duration/_speed);

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
        Collider[] enemyCollider = Physics.OverlapSphere(transform.position, attackRange, _whatIsEnemy);
		foreach (var enemyCol in enemyCollider)
		{
            if (enemyCol.TryGetComponent<HealthSystem>(out HealthSystem healthSystem))
			{
                healthSystem.Hp -= _damaged;
			}
		}
        SoundManager.Instance.PlaySFX(_attackAudioType, transform.position);
        this.Pop(_explosionEffectPoolType, transform.position, Quaternion.identity);
        transform.DOKill();
    }

    protected override void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * _speed);
        Gizmos.color = Color.white;
    }
}
