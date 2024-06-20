using System;
using System.Collections;
using Crogen.HealthSystem;
using Crogen.ObjectPooling;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class AgentBullet : MonoPoolingObject
{
    [SerializeField] private int _damaged = 1;
    public float lifeTime = 2f;
    public float speed = 80f;
    [SerializeField] protected LayerMask _whatIsOrigin;
    [SerializeField] protected PoolType _poolType;
    [SerializeField] protected PoolType _explosionEffect;
    private Collider[] _hitTarget;
    
    public override void OnPop()
    {
        _hitTarget = new Collider[1];
        StopAllCoroutines();
        
        Vector3 startPos = transform.position;
        Vector3 endPos = transform.forward.normalized * (speed * lifeTime);
        transform.DOMove(endPos + startPos, lifeTime).SetEase(Ease.OutCubic);
        StartCoroutine(AutoDieCoroutine());
    }

    public override void OnPush()
    { 
        StopAllCoroutines();
        transform.DOKill();
        this.Pop(_explosionEffect, transform.position, Quaternion.identity);
    }
    
    private void FixedUpdate()
    {
        if (Physics.OverlapBoxNonAlloc(transform.position, transform.localScale + Vector3.one*0.2f, _hitTarget, transform.rotation, ~_whatIsOrigin) > 0)
        {
            if (_hitTarget[0].TryGetComponent(out HealthSystem healthSystem))
            {
                healthSystem.Hp -= _damaged;
            }
            else
                _hitTarget[0].GetComponentInParent<HealthSystem>().Hp -= _damaged;
            Push(_poolType);
        }
    }

    private IEnumerator AutoDieCoroutine()
    {
        yield return new WaitForSeconds(lifeTime);
        Push(_poolType);
    }
}