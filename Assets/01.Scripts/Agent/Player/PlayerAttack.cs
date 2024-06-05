using UnityEngine;

public class PlayerAttack : AgentAttack
{
    //Managements
    private UIManager _uiManager;
    private CameraManager _cameraManager;
    
    private Collider[] _aroundEnemyCols;
    private bool _isAttack = false;

    [Header("PowerUp")]
    [SerializeField] private PoolType _powerUpBullet;
    [SerializeField] private GameObject _powerUpSkinnedEffect;
    private PoolType _defaultBulletType;
    
    private bool _powerUp;
    public bool PowerUp
    {
        get=>_powerUp;
        set
        {
            if (value == true)
            {
                _bulletType = _powerUpBullet;
            }
            else
            {
                _bulletType = _defaultBulletType;
            }
            _powerUpSkinnedEffect.SetActive(value);
            _powerUp = value;
        }
    }

    [Header("Barrier")] 
    [SerializeField] private GameObject _barrierEffect;
    
    private bool _barrier;
    public bool Barrier
    {
        get => _barrier;
        set
        {
            _barrier = value;
            _barrierEffect.SetActive(_barrier);
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
        if (Input.GetKeyDown(KeyCode.R))
        {
            PowerUp = !PowerUp;
            Debug.Log("PowerUp : " + PowerUp);
        }
        
        if (_isAttack)
        {
            OnAttack();
            _cameraManager.SetPlayerCameraShack(0.3f, 0.5f, 100);
        }
    }
    
    private void FixedUpdate()
    {
        Physics.OverlapSphereNonAlloc(transform.position, _speed * _bulletLifeTime, _aroundEnemyCols, _whatIsEnemy);
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