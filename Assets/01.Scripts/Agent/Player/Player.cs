using Cinemachine;
using Crogen.AgentFSM;
using UnityEngine;

public class Player : Agent<AgentStateEnum>
{
    private ItemEffect _currentItemEffect;

    public ItemEffect CurrentItemEffect
    {
        get => _currentItemEffect;
        set
        {
            if (_currentItemEffect != null)
            {
                _currentItemEffect.EnableEffect();
                //효과 실행
            }
            _currentItemEffect = value;
        }
    }
    public PlayerAttack PlayerAttack { get; private set; }
    [SerializeField] private CinemachineVirtualCamera _playerDieVirtualCamera;
    
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