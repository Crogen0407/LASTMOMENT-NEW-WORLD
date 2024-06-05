using Cinemachine;
using Crogen.AgentFSM;
using Crogen.HealthSystem;
using UnityEngine;

public class Player : Agent<AgentStateEnum>
{
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