using System.Collections;
using Crogen.ObjectPooling;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class BossAttack : MonoBehaviour
{
    [Header("Bullet")]
    [SerializeField] private Transform[] _attackTrms;
    [SerializeField] private PoolType _attackBulletPoolType;
    [SerializeField] private float _bulletSpeed = 150f;
    [SerializeField] private float _bulletDuration = 10f;

    [Header("Laser")] 
    [SerializeField] private LaserGroup[] _laserGroups;

    private void Awake()
    {
        for (int i = 0; i < _laserGroups.Length; ++i)
        {
            _laserGroups[i].SetLaserActive(false);
            _laserGroups[i].ChargeEffectActive(false);
        }
    }

    public void ShootLaser(float duration, UnityAction endEvent)
    {
        StartCoroutine(CoroutineShootLaser(duration, endEvent));
    }

    private IEnumerator CoroutineShootLaser(float duration, UnityAction endEvent)
    {
        //Charge
        foreach (var t in _laserGroups)
            t.ChargeEffectActive(true);
        yield return new WaitForSeconds(5);
        foreach (var t in _laserGroups)
            t.ChargeEffectActive(false);
        
        CameraManager.Instance.SetPlayerCameraShack(2f, 10f, 10f);
        
        //Shoot
        foreach (var t in _laserGroups)
            t.SetLaserActive(true);
        yield return new WaitForSeconds(duration);
        foreach (var t in _laserGroups)
            t.SetLaserActive(false);
        endEvent?.Invoke();
    }
    
    public void ShootBullet()
    {
        foreach (var t in _attackTrms)
        {
            AgentBullet bullet = this.Pop(_attackBulletPoolType, t.position, t.rotation) as AgentBullet;

            bullet.transform.forward = t.transform.forward;

            bullet.transform.DOMove(transform.position + transform.forward * _bulletSpeed, _bulletDuration);
        }
    }
}
