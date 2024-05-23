public class PlayerAttack : AgentAttack
{
    private bool _isAttack = false;
    
    protected override void Awake()
    {
        base.Awake();
        _gameManager.InputReader.AttackStartEvent += HandleStartAttack;
        _gameManager.InputReader.AttackEndkEvent += HandleEndAttack;
    }

    private void OnDestroy()
    {
        _gameManager.InputReader.AttackStartEvent -= HandleStartAttack;
        _gameManager.InputReader.AttackEndkEvent -= HandleEndAttack;
    }

    protected override void Update()
    {
        base.Update();
        if (_isAttack)
        {
            OnAttack();
        }
    }

    private void HandleStartAttack()
    {
        _isAttack = true;
    }

    private void HandleEndAttack()
    {
        _isAttack = false;
    }
}