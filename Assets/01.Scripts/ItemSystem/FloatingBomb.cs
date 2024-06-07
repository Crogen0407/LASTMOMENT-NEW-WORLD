using System.Collections;
using Crogen.HealthSystem;
using Crogen.ObjectPooling;
using UnityEngine;

public class FloatingBomb : MonoPoolingObject
{
    [SerializeField] private float _damage = 80f;
    [SerializeField] private PoolType _explosionEffect;
    [SerializeField] private float _range = 25f;
    
    //Components
    private Collider _collider;

    private void HandleExplosion()
    {
        Collider[] colliders = new Collider[20];

        int count = Physics.OverlapSphereNonAlloc(transform.position, _range, colliders);

        this.Pop(_explosionEffect, transform.position, Quaternion.identity);
        
        for (int i = 0; i < count; ++i)
        {
            if (colliders[i].TryGetComponent(out HealthSystem healthSystem))
            {
                healthSystem.Hp -= _damage;
            }
        }
        Push(PoolType.FloatingBomb);
    }

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    public override void OnPop()
    {
        StartCoroutine(CoroutineOnPop());
    }

    public override void OnPush()
    {
    }

    private void OnCollisionEnter(Collision other)
    {
        HandleExplosion();
    }

    private IEnumerator CoroutineOnPop()
    {
        _collider.enabled = false;
        yield return new WaitForSeconds(1);
        _collider.enabled = true;
        TalkContent.Instance.OnTalk("System", "지뢰 설치 완료");
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _range);
    }
}
