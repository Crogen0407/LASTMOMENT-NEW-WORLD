using Cinemachine;
using Crogen.AgentFSM;
using UnityEngine;

public class Player : Agent<AgentStateEnum>
{
    public PlayerAttack PlayerAttack { get; private set; }
    [SerializeField] private CinemachineVirtualCamera _playerDieVirtualCamera;
    
    [Header("PowerUp")]
    [SerializeField] private GameObject _powerUpSkinnedEffect;
    
    private bool _powerUp;
    public bool PowerUp
    {
        get=>_powerUp;
        set
        {
            PlayerAttack.ChangeBulletEffect(value);
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
            HealthSystem.isImpassible = _barrier;
        }
    }
    
    protected override void Awake()
    {
        base.Awake();
        PlayerAttack = GetComponent<PlayerAttack>();
    }

    public override void SetDead()
    {
        base.SetDead();
        _playerDieVirtualCamera.m_Priority = 20;
    }
}