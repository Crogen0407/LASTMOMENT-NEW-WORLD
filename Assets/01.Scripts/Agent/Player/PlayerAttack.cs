using UnityEngine;

public class PlayerAttack : AgentAttack
{
    //Managements
    private UIManager _uiManager;
    private CameraManager _cameraManager;
    
    private Collider[] _aroundEnemyCols;
    private bool _isAttack = false;
    
    protected override void Awake()
    {
        base.Awake();
        _uiManager = UIManager.Instance;
        _cameraManager = CameraManager.Instance;
        
        _aroundEnemyCols = new Collider[20];
        _gameManager.InputReader.AttackStartEvent += HandleStartAttack;
        _gameManager.InputReader.AttackEndEvent += HandleEndAttack;
    }

    private void OnDestroy()
    {
        _gameManager.InputReader.AttackStartEvent -= HandleStartAttack;
        _gameManager.InputReader.AttackEndEvent -= HandleEndAttack;
    }

    protected override void Update()
    {
        base.Update();
        if (_isAttack)
        {
            OnAttack();
            _cameraManager.SetPlayerCameraShack(0.3f, 0.5f, 100);
        }
    }

    
    private void FixedUpdate()
    {
        Physics.OverlapSphereNonAlloc(transform.position, _speed * _bulletLifeTime, _aroundEnemyCols, _whatIsEnemy);
        
        _uiManager.UpdateEnemyAim(_aroundEnemyCols);
    }

    private void HandleStartAttack()
    {
        _isAttack = true;
    }

    private void HandleEndAttack()
    {
        _isAttack = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _speed * _bulletLifeTime);
    }
}