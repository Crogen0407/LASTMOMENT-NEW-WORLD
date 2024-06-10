using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolableVFX : MonoPoolingObject
{
    [SerializeField] private PoolType _poolType;
    [SerializeField] private float _lifeTime = 1f;
    private float _currentTime = 0;
    protected event Action _dieEvent;
    private List<ParticleSystem> _particle;

    private void Awake()
    {
        _particle  = GetComponentsInChildren<ParticleSystem>().ToList();
    }

    public override void OnPop()
    {
        StartCoroutine(CoroutineDie());
    }

    public override void OnPush()
    {
        _dieEvent?.Invoke();
        _currentTime = 0;
    }

    private IEnumerator CoroutineDie()
    {
        _particle.ForEach(x => x.Play());
        while (_lifeTime > _currentTime)
        {
            _currentTime += Time.deltaTime;
            yield return null;
        }
        
        foreach (var p in _particle)
        {
            p.Simulate(0);
        }
        Push(_poolType);
    }
}
