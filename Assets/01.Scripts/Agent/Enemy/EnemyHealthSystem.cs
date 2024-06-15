using Crogen.HealthSystem;
using Crogen.ObjectPooling;
using UnityEngine;

public class EnemyHealthSystem : HealthSystem
{
    [SerializeField] private PoolType _healEffectType;
    [SerializeField] private PoolType _dieEffectType;
    private Enemy _enemyBase;
    
    protected override void Awake()
    {
        base.Awake();
        _enemyBase = GetComponent<Enemy>();
    }

    
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Player"))
        {
            --Hp;
        }
        else if (other.transform.CompareTag("Untagged"))
        {
            Hp -= 1000;
        }
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
    }

    protected override void OnDie()
    {
        this.Pop(_dieEffectType, transform.position, Quaternion.identity);
        _enemyBase.SetDead();
        Destroy(gameObject);
    }
}
