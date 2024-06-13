using UnityEngine;

public class PlayerAttack : AgentAttack
{
    //Managements
    private UIManager _uiManager;
    private CameraManager _cameraManager;
    
    private Collider[] _aroundEnemyCols;
    private bool _isAttack = false;
    [SerializeField]private LayerMask _whatIsSelf;

    [SerializeField] private PoolType _powerUpBullet;
    private PoolType _defaultBulletType;
    [SerializeField] private LineRenderer _aimLineRenderer;
    [SerializeField] private Transform _aimTrm;
    public void ChangeBulletEffect(bool isPowerUp)
    {
        if (isPowerUp)
        {
            _bulletType = _powerUpBullet;
        }
        else
        {
            _bulletType = _defaultBulletType;
        }
    }
    
    protected override void Awake()
    {
        base.Awake();
        _uiManager = UIManager.Instance;
        _cameraManager = CameraManager.Instance;
        
        _aroundEnemyCols = new Collider[20];
        _gameManager.InputReader.AttackEvent += HandleAttack;
        _defaultBulletType = _bulletType;
    }

    private void OnDestroy()
    {
        _gameManager.InputReader.AttackEvent -= HandleAttack;
    }

    protected override void Update()
    {
        base.Update();
        
        if (_isAttack)
        {
            OnAttack();
            _cameraManager.SetPlayerCameraShack(Time.deltaTime, 0.3f, 0.4f);
        }
    }
    
    private void FixedUpdate()
    {
        Physics.OverlapSphereNonAlloc(transform.position, _speed * _bulletLifeTime, _aroundEnemyCols, _whatIsEnemy);

        Vector3 startPos = transform.forward + transform.position;
        Vector3 endPos = transform.position + transform.forward * (_speed * _bulletLifeTime);
        Ray ray = new Ray(startPos, transform.forward);
        
        _aimLineRenderer.SetPosition(0, startPos);
        if (Physics.Raycast(ray, out RaycastHit hit, _speed * _bulletLifeTime, ~_whatIsSelf))
        {
            _aimLineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            _aimLineRenderer.SetPosition(1, endPos);
        }
        _aimTrm.position = _aimLineRenderer.GetPosition(1);
        Debug.DrawRay(transform.position, transform.forward * (_speed * _bulletLifeTime));
    }

    private void HandleAttack(bool value)
    {
        _isAttack = value;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _speed * _bulletLifeTime);
    }
}