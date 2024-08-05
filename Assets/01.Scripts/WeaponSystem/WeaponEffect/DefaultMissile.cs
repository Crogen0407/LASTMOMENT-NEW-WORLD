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
        SoundManager.Instance.PlaySFX(_fireAudioType, transform.position);
        _targetPos = transform.position + transform.forward * duration * _speed;
        transform.DOMove(_targetPos, duration);
    }

    private void OnCollisionEnter(Collision other)
    {

        if (other.transform.TryGetComponent(out HealthSystem healthSystem))
        {
            healthSystem.Hp -= _damaged;
        }
        else if (other.transform.transform.parent.TryGetComponent(out HealthSystem healthSystemInParent))
        {
            healthSystemInParent.Hp -= _damaged;
        }

        Destroy(gameObject);
    }

    private void Update()
	{
        if (duration < _curLifeTime)
        {
            Destroy(gameObject);
        }
        else
        {
            _curLifeTime += Time.deltaTime;
        }
    }

	private void FixedUpdate()
    {
        if(Physics.OverlapSphereNonAlloc(transform.position, attackRange, _attackTargets, _whatIsEnemy) > 0)
		{
            Destroy(gameObject);
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
