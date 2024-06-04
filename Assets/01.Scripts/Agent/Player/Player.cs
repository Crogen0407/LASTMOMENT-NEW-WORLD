using Crogen.AgentFSM;
using UnityEngine;

public class Player : Agent<AgentStateEnum>
{
    public PlayerAttack PlayerAttack { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        PlayerAttack = GetComponent<PlayerAttack>();
    }

    public override void SetDead()
    {
        
    }
}
