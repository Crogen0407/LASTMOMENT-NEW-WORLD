using Crogen.AgentFSM;
using UnityEngine;

public class Player : Agent<AgentStateEnum>
{
    #region Components
    public Rigidbody rigidbodyCompo { get; private set; }
    #endregion

    protected override void Awake()
    {
        rigidbodyCompo = GetComponent<Rigidbody>();
        base.Awake();
    }

    public override void SetDead()
    {
    }
}
