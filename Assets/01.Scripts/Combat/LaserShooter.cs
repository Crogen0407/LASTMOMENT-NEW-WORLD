using System;
using DG.Tweening;
using UnityEngine;

public class LaserShooter : MonoBehaviour
{
   private GameObject _laserEffect;
   private GameObject _chargeEffect;
   private Renderer _renderer;
   private int _dissolveValueID;
   
   private void Init()
   {
      _chargeEffect ??= transform.Find("ChargeEffect").gameObject;
      _laserEffect ??= transform.Find("Laser").gameObject;
      if (_renderer == null)
      {
         _renderer = GetComponent<Renderer>();
         _dissolveValueID = Shader.PropertyToID("_Value");
      }
   }

   /// <summary>
   /// 3초 동안 디졸브 되었다가 active를 설정한다.
   /// </summary>
   /// <param name="active"></param>
   public void SetDissolveActive(bool active)
   {
      Init();
      CoroutineDissolveActive(active);
   }
   
   public void ChargeEffectActive(bool active)
   {
      Init();
      _chargeEffect.SetActive(active);
   }

   private void CoroutineDissolveActive(bool active)
   {
      int activeInt = Convert.ToInt32(active);
      gameObject.SetActive(true);
      _renderer.material.SetFloat(_dissolveValueID, 1-activeInt);
      _renderer.material.DOFloat(activeInt, _dissolveValueID, 3f).OnComplete(() =>
      {
         gameObject.SetActive(active);
      });
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
}