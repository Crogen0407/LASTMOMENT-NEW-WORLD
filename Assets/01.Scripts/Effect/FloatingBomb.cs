using System;
using Crogen.HealthSystem;
using Crogen.ObjectPooling;
using UnityEngine;

public class FloatingBomb : MonoBehaviour
{
    [SerializeField] private float _damage = 80f;
    [SerializeField] private PoolType _explosionEffect;
    [SerializeField] private float _range = 25f;
    
    private void OnCollisionEnter(Collision other)
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
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _range);
    }
}
