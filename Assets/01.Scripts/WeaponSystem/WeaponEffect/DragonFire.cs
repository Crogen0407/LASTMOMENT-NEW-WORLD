using UnityEngine;
using Crogen.HealthSystem;

public class DragonFire : WeaponEffect
{
	[Header("---Dragon Fire---")]
	[SerializeField] private float _attackDelay = 0.1f;
	[SerializeField] private Vector3 _attackOverlapScale = Vector3.one;
	[SerializeField] private Vector3 _attackOverlapOffset;
	[SerializeField] private float _damage = 1f;
	private float _currentAttackTime = 0.0f;

	public override void Init(Vector3 attackDirection, Transform parent = null)
	{
		WeaponManager.Instance.currentWeaponStack.Push(this);

		transform.parent = parent;
		transform.localRotation = Quaternion.identity;
		_attackTargets = new Collider[_attackPossiableCount];
		transform.forward = attackDirection.normalized;
		CameraManager.Instance.SetPlayerCameraShack(0.5f, 10f, 10f);
		CameraManager.Instance.SetPlayerCameraShack(duration, 4f, 4f);

		SoundManager.Instance.PlaySFX(_fireAudioType, transform.position);
	}

	private void Update()
	{
		_currentAttackTime += Time.deltaTime;
		_curLifeTime += Time.deltaTime;
		if (_curLifeTime > duration)
		{
			Destroy(gameObject);
		}
		if (_currentAttackTime > _attackDelay)
		{
			OnDamage();
			_currentAttackTime = 0;
		}
	}

	private void OnDamage()
	{
		//SoundManager.Instance.PlaySFX(_attackAudioType, transform.position);
		Physics.OverlapCapsuleNonAlloc(transform.position, transform.position + transform.rotation * (Vector3.forward * _attackOverlapScale.z) + _attackOverlapOffset, 5f, _attackTargets, _whatIsEnemy);
		if (_attackTargets == null) return;

		foreach (var enemy in _attackTargets)
		{
			if (enemy == null) continue;
			HealthSystem hs = enemy.GetComponent<HealthSystem>();
			if (hs == null)
				hs = enemy.GetComponentInParent<HealthSystem>();
			if (hs!=null)
				hs.Hp -= _damage*_attackDelay / duration;
		}
	}

	protected override void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(transform.position + _attackOverlapOffset, _attackOverlapScale);
		Gizmos.DrawRay(transform.position, transform.rotation * Vector3.forward);
		Gizmos.color = Color.white;
	}

}
