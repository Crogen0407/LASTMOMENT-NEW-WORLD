using Crogen.ObjectPooling;
using DG.Tweening;
using UnityEngine;

public class FlareShooter : WeaponEffect
{
    [Header("---Flare---")]
    [SerializeField] private PoolType _flarePoolType;
    [SerializeField] protected int _cycleCount = 10;
    [SerializeField] protected float _shootDelay = 0.5f;
    private float _curDelay = 0;
    [SerializeField] private float _bulletDuration;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _bulletDamage = 5f;

	public override void Init(Vector3 attackDirection, Transform parent = null)
	{
		base.Init(attackDirection, parent);
	}

	private void Update()
    {
        _curDelay += Time.deltaTime;
        if (_curDelay > _shootDelay)
        {
            --_cycleCount;
            _curDelay = 0;
            UpdateAttackCycles();
        }
        if (_cycleCount <= 0)
        {
            Destroy(gameObject);   
        }
    }

    protected virtual void UpdateAttackCycles()
    {
        SoundManager.Instance.PlaySFX(_fireAudioType, transform.position);
    }

    protected void ShootFlare(Vector3 attackDirection)
    {
        Flare flare = this.Pop(_flarePoolType, transform.position, Quaternion.identity) as Flare;
        flare.damage = _bulletDamage;
        flare.transform.DOMove(transform.position + attackDirection.normalized * (_bulletDuration * _bulletSpeed), _bulletDuration).OnComplete(() =>
        {
            flare.Push(_flarePoolType);
        });
    }
}
