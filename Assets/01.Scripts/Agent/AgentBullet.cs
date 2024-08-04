using System.Collections;
using Crogen.HealthSystem;
using Crogen.ObjectPooling;
using DG.Tweening;
using UnityEngine;

public class AgentBullet : MonoPoolingObject
{
    [SerializeField] private int _damaged = 1;
    public float lifeTime = 2f;
    public float speed = 80f;
    [SerializeField] protected LayerMask _whatIsOrigin;
    [SerializeField] protected PoolType _poolType;
    [SerializeField] protected PoolType _explosionEffect;

    [Header("Sound Effect")] 
    [SerializeField] protected AudioType _enableSoundEffect;
    [SerializeField] protected AudioType _disableSoundEffect;
    
    private Collider[] _hitTarget;
    public override void OnPop()
    {
        _hitTarget = new Collider[1];
        StopAllCoroutines();
        
        SoundManager.Instance.PlaySFX(_enableSoundEffect, transform.position);
        
        Vector3 startPos = transform.position;
        Vector3 endPos = transform.forward.normalized * (speed * lifeTime);
        transform.DOMove(endPos + startPos, lifeTime).SetEase(Ease.OutCubic);
        StartCoroutine(AutoDieCoroutine());
    }

    public override void OnPush()
    {
        StopAllCoroutines();
        transform.DOKill();
    }
    
    private void FixedUpdate()
    {
        if (Physics.OverlapBoxNonAlloc(transform.position, transform.localScale + Vector3.one*0.2f, _hitTarget, transform.rotation, ~_whatIsOrigin) > 0)
        {
            if (_hitTarget[0].TryGetComponent(out HealthSystem healthSystem))
            {
                healthSystem.Hp -= _damaged;
            }
            else if (_hitTarget[0].transform.parent.TryGetComponent(out HealthSystem healthSystemInParent))
            {
                healthSystemInParent.Hp -= _damaged;
            }
            SoundManager.Instance.PlaySFX(_disableSoundEffect, transform.position, true, 0.05f, true, 50f);
            this.Pop(_explosionEffect, transform.position, Quaternion.identity);
            Push(_poolType);
        }
    }

    private IEnumerator AutoDieCoroutine()
    {
        yield return new WaitForSeconds(lifeTime);
        Push(_poolType);
    }
}