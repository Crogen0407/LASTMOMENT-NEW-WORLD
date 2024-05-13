using UnityEngine;

public abstract class AgentMovement : MonoBehaviour
{
    [field:SerializeField] public float Speed { get; private set; }
    
    public float X { get; protected set; }
    public float Y { get; protected set; }
    public Vector3 Direction { get; protected set; }
    protected Rigidbody _rbCompo;
    
    public virtual void Awake()
    {
        _rbCompo = GetComponent<Rigidbody>();
    }

    protected abstract void Move(Vector2 dir);
}
