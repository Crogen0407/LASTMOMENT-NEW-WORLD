using Crogen.HealthSystem;
using Crogen.ObjectPooling;
using UnityEngine;

public class PlayerHealthSystem : HealthSystem
{
    [SerializeField] private PoolType _healEffectType;
    [SerializeField] private PoolType _damagedEffectType;
    [SerializeField] private PoolType _dieEffectType;
    private Player _playerBase;
    
    protected override void Awake()
    {
        base.Awake();
        _playerBase = GetComponent<Player>();
    }

    protected override void OnHpChange()
    {
        
    }

    protected override void OnHpUp()
    {
        this.Pop(_healEffectType, transform.position, Quaternion.identity);
    }

    protected override void OnHpDown()
    {
        this.Pop(_damagedEffectType, transform.position, Quaternion.identity);
    }

    protected override void OnDie()
    {
        this.Pop(_dieEffectType, transform.position, Quaternion.identity);
        _playerBase.SetDead();
    }
}
