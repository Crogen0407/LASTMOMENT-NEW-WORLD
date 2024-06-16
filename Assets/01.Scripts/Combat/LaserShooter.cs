using UnityEngine;

public class LaserShooter : MonoBehaviour
{
   private GameObject _laserEffect;
   private GameObject _chargeEffect;

   public void ChargeEffectActive(bool active)
   {
      if (_chargeEffect == null)
         _chargeEffect = transform.Find("ChargeEffect").gameObject;
      _chargeEffect.SetActive(active);
   }
   
   public void SetLaserEffectActive(bool active)
   {
      if (_laserEffect == null)
         _laserEffect = transform.Find("Laser").gameObject;
      _laserEffect.SetActive(active);
   }
}