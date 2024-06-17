using System;
using DG.Tweening;
using UnityEngine;

public class LaserShooter : MonoBehaviour
{
   private GameObject _laserEffect;
   private GameObject _chargeEffect;

   private void Init()
   {
      if (_chargeEffect == null)
         _chargeEffect = transform.Find("ChargeEffect").gameObject;
      if (_laserEffect == null)
         _laserEffect = transform.Find("Laser").gameObject;
   }
   
   public void ChargeEffectActive(bool active)
   {
      Init();
      _chargeEffect.SetActive(active);
   }
   
   public void FadeLaserEffect(float fadeTime = 1f, float duration = 2f)
   {
      Init();
      _laserEffect.transform.localScale = Vector3.zero;
      _laserEffect.SetActive(true);

      Sequence seq = DOTween.Sequence();
      
      seq.Append(_laserEffect.transform.DOScale(Vector3.one, fadeTime));
      seq.AppendInterval(duration);
      seq.Append(_laserEffect.transform.DOScale(Vector3.zero, fadeTime));
      seq.AppendCallback(() => _laserEffect.SetActive(false));
   }

   public void SetLaserEffectActive(bool active)
   {
      Init();
      _laserEffect.SetActive(active);
   }
}