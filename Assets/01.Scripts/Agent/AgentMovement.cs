using System;
using Crogen.AgentFSM;
using UnityEngine;

public class AgentMovement<T> : MonoBehaviour where T : Enum
{
    [field:SerializeField] public int MaxSpeed { get; protected set; }
    [field:SerializeField] public int CurSpeed { get; protected set; }
    [field:SerializeField] public float RotateSpeedX { get; protected set; } = 20f;
    [field:SerializeField] public float RotateSpeedY { get; protected set; } = 100f;
    
    public float X { get; protected set; }
    public float Y { get; protected set; }
    public Vector3 Direction { get; protected set; }

    #region Components
    protected Agent<T> _baseAgent { get; private set; }
    protected Rigidbody _rbCompo { get; private set; }
    #endregion
    
    public virtual void Awake()
    {
        _rbCompo = GetComponent<Rigidbody>();
        _baseAgent = GetComponent<Agent<T>>();
    }

    protected virtual void HandleMove(Vector2 dir){}
}
