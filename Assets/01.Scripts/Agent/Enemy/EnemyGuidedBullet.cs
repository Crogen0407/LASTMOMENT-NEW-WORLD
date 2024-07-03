using System.Collections;
using Crogen.ObjectPooling;
using DG.Tweening;
using UnityEngine;

public class EnemyGuidedBullet : MonoPoolingObject
{
    public float readyLifeTime = 0.8f;
    public float readyMoveLength = 3f;
    public float lifeTime = 2f;
    public float speed = 80f;
    [SerializeField] private LayerMask _whatIsPlayer;
    [SerializeField] protected PoolType _poolType;
    [SerializeField] protected PoolType _explosionEffect;
    [SerializeField] private float _findRadius = 200f;
    [SerializeField] private Collider[] _findTarget;
    
    public override void OnPop()
    {
        _findTarget = new Collider[1];

        if (Physics.OverlapSphereNonAlloc(transform.position, _findRadius, _findTarget, _whatIsPlayer) > 0)
        {
            GuidedMove(transform.position, _findTarget[0].transform);
            return;
        }
        
        Vector3 startPos = transform.position;
        Vector3 endPos = transform.forward.normalized * (speed * lifeTime);
        transform.DOMove(endPos + startPos, lifeTime).SetEase(Ease.OutCubic);
        StartCoroutine(AutoDieCoroutine());
    }
    
    private void GuidedMove(Vector3 startPos, Transform targetTrm)
    {
        //Ready
        Sequence seq = DOTween.Sequence();
        Vector3 endPos = transform.forward.normalized * (speed * lifeTime);
        seq.Append(transform.DOMove(endPos + startPos, lifeTime).SetEase(Ease.OutCubic));

        //Attack
        startPos = transform.position;
        endPos = targetTrm.position;
        seq.Append(transform.DOMove(startPos + endPos, lifeTime));
        seq.OnUpdate()

        seq.AppendCallback(() => Push(_poolType));
    }
    
    public override void OnPush()
    { 
        StopAllCoroutines();
        transform.DOKill();
        this.Pop(_explosionEffect, transform.position, Quaternion.identity);
    }

    private void OnCollisionEnter()
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
