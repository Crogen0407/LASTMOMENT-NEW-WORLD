using Crogen.ObjectPooling;
using UnityEngine;

public class AgentAttack : MonoBehaviour
{
    [SerializeField] protected Transform[] _BulletSpawnPoint;
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
        _currentDelayTime = 0;
        for (int i = 0; i < _BulletSpawnPoint.Length; ++i)
        {
            AgentBullet bullet = this.Pop(
                    _bulletType, 
                    _BulletSpawnPoint[i].position, 
                    _BulletSpawnPoint[i].rotation) as AgentBullet;
        }
    }

    protected virtual void Update()
    {
        _currentDelayTime += Time.deltaTime;
    }
}
