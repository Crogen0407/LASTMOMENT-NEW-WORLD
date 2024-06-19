using System.Collections;
using Crogen.ObjectPooling;
using UnityEngine;

public class AgentAttack : MonoBehaviour
{
    [SerializeField] protected Transform[] _bulletSpawnPoint;
    [SerializeField] protected PoolType _bulletType;
    
    [SerializeField] protected float _speed = 160f;
    [SerializeField] protected float _bulletLifeTime = 2f;
    
    protected GameManager _gameManager;
    public float delayTime = 5f;
    private float _currentDelayTime = 0;
    [SerializeField] protected LayerMask _whatIsEnemy; 
    
    protected virtual void Awake()
    {
        _gameManager = GameManager.Instance;
    }

    public float GetAttackRange() => _bulletLifeTime * _speed;
    
    public virtual void OnAttack()
    {
        if (_currentDelayTime < delayTime) return;
        StopAllCoroutines();
        _currentDelayTime = 0;
        StartCoroutine(CoroutineAttack());
    }

    private IEnumerator CoroutineAttack()
    {
        for (int i = 0; i < _bulletSpawnPoint.Length; ++i)
        {
            this.Pop(_bulletType, 
                _bulletSpawnPoint[i].position, 
                _bulletSpawnPoint[i].rotation);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    protected virtual void Update()
    {
        _currentDelayTime += Time.deltaTime;
    }
}
