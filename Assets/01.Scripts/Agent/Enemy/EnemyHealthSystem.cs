using Crogen.HealthSystem;
using Crogen.ObjectPooling;
using UnityEngine;

public class EnemyHealthSystem : HealthSystem
{
    [SerializeField] private PoolType _healEffectType;
    [SerializeField] private PoolType _damagedEffectType;
    [SerializeField] private PoolType _dieEffectType;
    private Enemy _enemyBase;
    
    protected override void Awake()
    {
        base.Awake();
        _enemyBase = GetComponent<Enemy>();
    }

    protected override void OnHpChange()
    {
        
    }

    protected override void OnHpUp()
    {
        this.Pop(_healEffectType, transform);
    }

    protected override void OnHpDown()
    {
        this.Pop(_damagedEffectType, transform.position, Quaternion.identity);
    }

    protected override void OnDie()
    {
        this.Pop(_dieEffectType, transform.position, Quaternion.identity);
        _enemyBase.SetDead();
    }
}
