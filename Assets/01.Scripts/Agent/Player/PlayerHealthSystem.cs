using Crogen.HealthSystem;
using Crogen.ObjectPooling;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthSystem : HealthSystem
{
    [SerializeField] private PoolType _healEffectType;
    [SerializeField] private PoolType _damagedEffectType;
    [SerializeField] private PoolType _dieEffectType;
    [SerializeField] private Slider _hpSlider;
    private Player _playerBase;
    
    protected override void Awake()
    {
        base.Awake();
        _playerBase = GetComponent<Player>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Enemy") || other.transform.CompareTag("Untagged"))
        {
            Hp = 0;
        }
    }
    
    protected override void OnHpChange()
    {
        _hpSlider.value = (float)Hp / maxHp;
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
