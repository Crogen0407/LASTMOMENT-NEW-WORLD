using System;
using System.Collections;
using System.Collections.Generic;
using Crogen.HealthSystem;
using Crogen.ObjectPooling;
using DG.Tweening;
using UnityEngine;

public class EnemyGuidedBullet : MonoPoolingObject
{
    [SerializeField] private int _damaged = 1;
    public float lifeTime = 2f;
    public float speed = 80f;
    [SerializeField] protected LayerMask _whatIsOrigin;
    [SerializeField] private LayerMask _whatIsPlayer;
    [SerializeField] protected PoolType _poolType;
    [SerializeField] protected PoolType _explosionEffect;
    private Collider[] _hitTarget;
    [SerializeField] private float _findRadius = 200f;
    [SerializeField] private Collider[] _findTarget;
    
    public override void OnPop()
    {
        _hitTarget = new Collider[1];
        _findTarget = new Collider[1];

        if (Physics.OverlapSphereNonAlloc(transform.position, _findRadius, _findTarget, _whatIsPlayer) > 0)
        {
            Vector3 findPos = _findTarget[0].transform.position;
            StartCoroutine(GuidedMove(transform.position, findPos));
            return;
        }
        
        Vector3 startPos = transform.position;
        Vector3 endPos = transform.forward.normalized * (speed * lifeTime);
        transform.DOMove(endPos + startPos, lifeTime).SetEase(Ease.OutCubic);
        StartCoroutine(AutoDieCoroutine());
    }


    private IEnumerator GuidedMove(Vector3 startPos, Vector3 endPos)
    {
        float currentTime = 0;
        float percent = 0;
        float lifeTime = Vector3.Distance(startPos, endPos)/speed;
        Vector3 lastPos = transform.position;
        while (percent < 1f)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / lifeTime;
            transform.position = Vector3.Slerp(startPos, endPos, percent);
            transform.forward = -(lastPos - transform.position).normalized;
            yield return null;
        }
        transform.position = endPos;
        
        yield return new WaitForSeconds(lifeTime);
        Push(_poolType);
    }
    
    public override void OnPush()
    { 
        StopAllCoroutines();
        transform.DOKill();
        this.Pop(_explosionEffect, transform.position, Quaternion.identity);
    }

    private void OnCollisionEnter(Collision other)
    {
        Push(_poolType);
    }

    private IEnumerator AutoDieCoroutine()
    {
        yield return new WaitForSeconds(lifeTime);
        Push(_poolType);
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(transform.position, _findRadius);
    }
}
