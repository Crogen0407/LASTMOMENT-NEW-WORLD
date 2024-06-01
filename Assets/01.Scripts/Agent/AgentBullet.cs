using System;
using System.Collections;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class AgentBullet : MonoPoolingObject
{
    [SerializeField] protected float _lifeTime = 5f;
    [SerializeField] protected float _speed = 80f;
    [SerializeField] protected LayerMask _whatIsSelf;
    [SerializeField] protected PoolType _poolType;
    
    public override void OnPop()
    {
        StartCoroutine(AutoDieCoroutine());
    }

    public override void OnPush()
    { 
        
    }

    private Collider[] _hitTarget = new Collider[1];
    
    private void Update()
    {
        if (Physics.OverlapBoxNonAlloc(transform.position, transform.localScale, _hitTarget, transform.rotation, ~_whatIsSelf) > 0)
        {
            Debug.Log(_hitTarget[0].gameObject.name);
        }
    }

    private IEnumerator AutoDieCoroutine()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = transform.forward * (_speed * _lifeTime);
        transform.DOMove(endPos + startPos, _lifeTime);
        
        yield return new WaitForSeconds(_lifeTime);
        Push(_poolType);
    }
}