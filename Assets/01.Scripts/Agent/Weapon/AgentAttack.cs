using Crogen.ObjectPooling;
using UnityEngine;

public class AgentAttack : MonoBehaviour
{
    [SerializeField] protected Transform[] _BulletSpawnPoint;
    [SerializeField] protected PoolType _bulletType;
    protected GameManager _gameManager;
    public float delayTime = 5f;
    private float _currentDelayTime = 0;

    protected virtual void Awake()
    {
        _gameManager = GameManager.Instance;
    }

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
