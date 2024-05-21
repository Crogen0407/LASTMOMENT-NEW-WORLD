using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class AgentBullet : MonoPoolingObject
{
    [SerializeField] protected float _lifeTime = 5f;
    [SerializeField] protected float _speed = 80f;
    
    public override void OnPop()
    {
        StartCoroutine(AutoDieCoroutine());
    }

    public override void OnPush()
    {
        
    }

    private IEnumerator AutoDieCoroutine()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = transform.forward * (_speed * _lifeTime);
        transform.DOMove(endPos + startPos, _lifeTime);
        
        yield return new WaitForSeconds(_lifeTime);
        Push(PoolType.PlayerDefualtBullet);
    }
}