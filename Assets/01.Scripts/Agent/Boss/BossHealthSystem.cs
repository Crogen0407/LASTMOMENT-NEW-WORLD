using Crogen.HealthSystem;

public class BossHealthSystem : HealthSystem
{
    private UIManager _uiManager;

    protected override void Awake()
    {
        base.Awake();
        _uiManager = UIManager.Instance;
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
    }
}
