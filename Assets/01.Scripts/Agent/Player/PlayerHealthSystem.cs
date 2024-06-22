using System.Collections;
using Crogen.HealthSystem;
using Crogen.ObjectPooling;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthSystem : HealthSystem
{
    [SerializeField] private GameObject _collider;
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
        if (other.transform.CompareTag("Boss"))
        {
            Hp -= 1000;
            return;
        }
        if (other.transform.TryGetComponent(out HealthSystem otherHealth))
        {
            if (otherHealth.Hp > Hp) //내가 터지고
            {
                Hp -= maxHp/2f;
                otherHealth.Hp -= otherHealth.maxHp;
                CameraManager.Instance.SetPlayerCameraShack(1, 5, 10);
            }
            else if (otherHealth.Hp < Hp) //상대가 터지고
            {
                var temp = otherHealth.Hp;
                otherHealth.Hp -= otherHealth.maxHp/2f;
                Hp -= temp;
                CameraManager.Instance.SetPlayerCameraShack(1, 2, 5);
            }
            else //둘다 터진다.
            {
                Hp -= maxHp;
                otherHealth.Hp -= otherHealth.maxHp/2f;
                CameraManager.Instance.SetPlayerCameraShack(1, 5, 10);
            }
            StartCoroutine(CoroutineImpassible());
            this.Pop(PoolType.vfx_EnergyExplosion, transform.position, Quaternion.identity);
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

    private IEnumerator CoroutineImpassible()
    {
        _collider.SetActive(false);
        isImpassible = true;
        yield return new WaitForSeconds(1f);
        isImpassible = false;
        _collider.SetActive(true);
    }
}
