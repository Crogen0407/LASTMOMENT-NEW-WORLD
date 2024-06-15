using Crogen.ObjectPooling;
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
    [SerializeField] private PoolType _attackEffectPoolType;
    
    public void Attack()
    {
        for (int i = 0; i < _attackTrms.Length; ++i)
        {
            this.Pop(_attackEffectPoolType, _attackTrms[i].position, _attackTrms[i].rotation);
        }
    }    
}
