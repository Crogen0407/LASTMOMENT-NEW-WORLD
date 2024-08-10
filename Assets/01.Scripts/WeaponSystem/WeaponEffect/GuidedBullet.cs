using Crogen.HealthSystem;
using Crogen.ObjectPooling;
using DG.Tweening;
using UnityEngine;

public class GuidedBullet : WeaponEffect
{
    private Collider _attackTarget;
    private bool _isTargetting;
    [SerializeField] private PoolType _explosionEffectPoolType;
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _damaged = 10f;
    
    public override void Init(Vector3 attackDirection, Transform parent = null)
    {
        base.Init(attackDirection, null);

        _attackTargets = new Collider[1];
        SoundManager.Instance.PlaySFX(_fireAudioType, transform.position);
        transform.DOMove(transform.position + (transform.forward * duration * _speed), duration);
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
        if (_isTargetting) return;

        if (Physics.OverlapSphereNonAlloc(transform.position, attackRange, _attackTargets, _whatIsEnemy) > 0)
		{
            _isTargetting = true;
            transform.DOKill();
            transform.DOMove(_attackTargets[0].transform.position, duration / _speed);
            transform.forward = (_attackTargets[0].transform.position - transform.position).normalized;
        }
    }

    private void OnDestroy()
    {
        SoundManager.Instance.PlaySFX(_attackAudioType, transform.position);
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
