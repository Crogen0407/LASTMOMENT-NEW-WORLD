using UnityEngine;

public class AgentAttack : MonoBehaviour
{
    [SerializeField] protected Transform[] _BulletSpawnPoint;
    
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
    }

    protected virtual void Update()
    {
        _currentDelayTime += Time.deltaTime;
    }
}
