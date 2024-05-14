using Crogen.AgentFSM;
using Crogen.PowerfulInput;
using UnityEngine;

public class Player : Agent<AgentStateEnum>
{
    //Values
    [HideInInspector] public Vector2 lastMousePosition;
    [HideInInspector] public Vector3 lookAngle = Vector3.zero;
    
    [field:SerializeField] public int MaxSpeed { get; set; }
    [field:SerializeField] public int CurSpeed { get; set; }
    [field:SerializeField] public float RotateSpeedX { get; set; } = 20f;
    [field:SerializeField] public float RotateSpeedY { get; set; } = 100f;
    
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
