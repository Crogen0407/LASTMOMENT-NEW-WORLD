using UnityEngine;

public abstract class WeaponEffect : MonoBehaviour
{
    protected Collider[] _attackTargets;
    [SerializeField] protected LayerMask _whatIsEnemy;
    [SerializeField] private int _attackPossiableCount = 10;
    public float attackRange = 30f;
    public float duration = 10f;
    protected float _curLifeTime = 0;
    
    public virtual void Init(Vector3 attackDirection)
    {
        _attackTargets = new Collider[_attackPossiableCount];
        transform.forward = attackDirection.normalized;

        Physics.OverlapSphereNonAlloc(transform.position, attackRange, _attackTargets, _whatIsEnemy);
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.white;
    }
}
