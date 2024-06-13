using Crogen.ObjectPooling;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
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
