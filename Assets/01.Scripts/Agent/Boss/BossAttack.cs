using System;
using System.Collections;
using Crogen.ObjectPooling;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class BossAttack : MonoBehaviour
{
    [Header("Bullet")]
    [SerializeField] private Transform[] _bulletAttackTrms;
    [SerializeField] private PoolType _attackBulletPoolType;

    [Header("Laser")] 
    [SerializeField] private LaserGroup[] _laserGroups;

    private void Awake()
    {
        for (int i = 0; i < _laserGroups.Length; ++i)
        {
            _laserGroups[i].ChargeEffectActive(false);
            _laserGroups[i].SetLaserShooterActive(false);
        }
    }

    public void ShootLaser(float duration, UnityAction endEvent)
    {
        StartCoroutine(CoroutineShootLaser(duration, endEvent));
    }

    private IEnumerator CoroutineShootLaser(float duration, UnityAction endEvent)
    {
        //Active
        foreach (var t in _laserGroups)
            t.SetDissolveLaserShooterActive(true);
        yield return new WaitForSeconds(3f);
        
        //Charge
        foreach (var t in _laserGroups)
            t.ChargeEffectActive(true);
        yield return new WaitForSeconds(5);
        foreach (var t in _laserGroups)
            t.ChargeEffectActive(false);
        
        CameraManager.Instance.FadePlayerCameraShack(duration + 5f, 10f, 10f);
        
        //Shoot
        foreach (var t in _laserGroups)
            t.FadeLaserEffect(1f, duration);
        yield return new WaitForSeconds(duration);
        
        //Active
        foreach (var t in _laserGroups)
            t.SetDissolveLaserShooterActive(false);
        yield return new WaitForSeconds(3f);
        
        endEvent?.Invoke();
    }
    
    public void ShootBullet(float attackDelay, float duration, Action endEvent = null)
    {
        StartCoroutine(CoroutineShootBullet(attackDelay, duration, endEvent));
    }

    private IEnumerator CoroutineShootBullet(float attackDelay, float duration, Action endEvent = null)
    {
        float currentTime = 0f;
        float delayTime = 0;
        while(currentTime < duration)
        {
            currentTime += Time.deltaTime;
            delayTime += Time.deltaTime;
            if (delayTime > attackDelay)
            {
                foreach (var t in _bulletAttackTrms)
                {
                    this.Pop(_attackBulletPoolType, t.position+t.forward, t.localRotation);
                }                
                delayTime = 0;
            }

            yield return null;
        }
        yield return new WaitForSeconds(duration);
        endEvent?.Invoke();
    }
}
