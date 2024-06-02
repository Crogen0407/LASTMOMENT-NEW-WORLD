using System;
using System.Collections;
using UnityEngine;

public class PoolableVFX : MonoPoolingObject
{
    [SerializeField] private PoolType _poolType;
    [SerializeField] private float _lifeTime = 1f;
    private float _currentTime = 0;
    protected event Action _dieEvent;
    
    public override void OnPop()
    {
        StartCoroutine(CoroutineDie());
    }

    public override void OnPush()
    {
        _dieEvent?.Invoke();
    }

    private IEnumerator CoroutineDie()
    {
        while (_lifeTime > _currentTime)
        {
            _currentTime += Time.deltaTime;
            yield return null;
        }
        this.Push(_poolType);
    }
}
