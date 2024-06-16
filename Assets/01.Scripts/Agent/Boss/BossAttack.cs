using Crogen.ObjectPooling;
using DG.Tweening;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
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
