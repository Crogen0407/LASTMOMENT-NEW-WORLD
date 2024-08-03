using Crogen.HealthSystem;
using UnityEngine;

public class BossHealthSystem : HealthSystem
{
    private UIManager _uiManager;
    private Boss _bossBase;
    [SerializeField] private ParticleSystem _dieEffect;

    protected override void Awake()
    {
        base.Awake();
        _uiManager = UIManager.Instance;                   
        _bossBase = GetComponent<Boss>();
    }

    protected override void OnHpChange()
    {
        _uiManager.SetBossUI();
    }

    protected override void OnHpUp()
    {
    }

    protected override void OnHpDown()
    {
    }

    protected override void OnDie()
    {
        var dieEffect = Instantiate(_dieEffect, transform.position, Quaternion.identity);
        dieEffect.Play(true);
        UIManager.Instance.CloseBossUI();
        _bossBase.SetDead();
    }
}
