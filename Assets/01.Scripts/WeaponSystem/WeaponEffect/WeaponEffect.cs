using UnityEngine;

public abstract class WeaponEffect : MonoBehaviour
{
    [SerializeField] protected AudioType _attackAudioType;
    [SerializeField] protected AudioType _fireAudioType;

    protected Collider[] _attackTargets;
    [SerializeField] protected LayerMask _whatIsEnemy;
    [SerializeField] protected int _attackPossiableCount = 10;
    public float attackRange = 30f;
    public float duration = 10f;
    protected float _curLifeTime = 0;

    
    //플레어는 부모를 사용하지만 일반 총알은 사용하지 않는다.
    public virtual void Init(Vector3 attackDirection, Transform parent = null)
    {
        WeaponManager.Instance.currentWeaponStack.Push(this);

        transform.parent = parent;
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
