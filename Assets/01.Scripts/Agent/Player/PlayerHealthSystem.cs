using Crogen.HealthSystem;
using Crogen.ObjectPooling;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthSystem : HealthSystem
{
    [SerializeField] private PoolType _healEffectType;
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
        if (other.transform.TryGetComponent(out HealthSystem otherHealth))
        {
            if (otherHealth.Hp > Hp) //내가 터지고
            {
                var temp = Hp;
                Hp -= maxHp/2f;
                otherHealth.Hp -= temp / 2f;
            }
            else if (otherHealth.Hp < Hp) //상대가 터지고
            {
                var temp = otherHealth.Hp;
                otherHealth.Hp -= otherHealth.maxHp/2f;
                Hp -= temp / 2f;
            }
            else //둘다 터진다.
            {
                Hp -= maxHp / 2f;
                otherHealth.Hp -= otherHealth.maxHp / 2f;
            }
        }
        else if (other.transform.CompareTag("Untagged"))
        {
            Hp -= 1000;
        }
    }
    
    protected override void OnHpChange()
    {   
        _hpSlider.value = Hp / maxHp;
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
        _playerBase.SetDead();
    }
}
