using System;
using System.Collections;
using Crogen.HealthSystem;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class AgentBullet : MonoPoolingObject
{
    [SerializeField] private int _damaged = 1;
    [HideInInspector] public float lifeTime = 2f;
    [HideInInspector] public float speed = 80f;
    [SerializeField] protected LayerMask _whatIsOrigin;
    [SerializeField] protected PoolType _poolType;
    
    public override void OnPop()
    {
        StartCoroutine(AutoDieCoroutine());
    }

    public override void OnPush()
    { 
        
    }

    private Collider[] _hitTarget = new Collider[1];
    
    private void FixedUpdate()
    {
        if (Physics.OverlapBoxNonAlloc(transform.position, transform.localScale + Vector3.one*0.1f, _hitTarget, transform.rotation, ~_whatIsOrigin) > 0)
        {
            if (_hitTarget[0].TryGetComponent(out HealthSystem healthSystem))
            {
                healthSystem.Hp -= _damaged;
            }
            StopAllCoroutines();
            Push(_poolType);
        }
    }

    private IEnumerator AutoDieCoroutine()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = transform.forward * (speed * lifeTime);
        transform.DOMove(endPos + startPos, lifeTime).SetEase(Ease.OutCubic);
        
        yield return new WaitForSeconds(lifeTime);
        Push(_poolType);
    }
}