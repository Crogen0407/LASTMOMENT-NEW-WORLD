using Crogen.ObjectPooling;
using DG.Tweening;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public BossPattern currentPattern;
    public BossPattern[] patterns;
    private int _currentPatternIndex;

    public int CurrentPatternIndex
    {
        get => _currentPatternIndex;
        set
        {
            _currentPatternIndex = Mathf.Clamp(value, 0, patterns.Length);
            currentPattern = patterns[_currentPatternIndex];
        }
    }
    
    [SerializeField] private Transform[] _attackTrms;
    [SerializeField] private PoolType _attackBulletPoolType;
    [SerializeField] private float _bulletSpeed = 150f;
    [SerializeField] private float _bulletDuration = 10f;
    
    public void Attack()
    {
        foreach (var t in _attackTrms)
        {
            AgentBullet bullet = this.Pop(_attackBulletPoolType, t.position, t.rotation) as AgentBullet;

            bullet.transform.forward = t.transform.forward;

            bullet.transform.DOMove(transform.position + transform.forward * _bulletSpeed, _bulletDuration);
        }
    }    
}
