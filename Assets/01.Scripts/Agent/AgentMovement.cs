using UnityEngine;

public class AgentMovement : MonoBehaviour
{
    
    [field:SerializeField] public int MaxSpeed { get; protected set; }
    [field:SerializeField] public int CurSpeed { get; protected set; }
    
    public float X { get; protected set; }
    public float Y { get; protected set; }
    public Vector3 Direction { get; protected set; }

    #region Components
    protected Agent _baseAgent { get; private set; }
    protected Rigidbody _rbCompo { get; private set; }
    #endregion
    
    public virtual void Awake()
    {
        _rbCompo = GetComponent<Rigidbody>();
        _baseAgent = GetComponent<Agent>();
    }

    protected virtual void HandleMove(Vector2 dir){}
}
