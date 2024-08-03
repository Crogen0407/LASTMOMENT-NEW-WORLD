using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShooter : MonoBehaviour
{
	[SerializeField] private float _delayTime = 12f;
	[SerializeField] private float _laserLifeTime = 8f;
    [SerializeField] private Transform _firePoint;
	[SerializeField] private GameObject _LaserEffect;
	[SerializeField] private ParticleSystem _chargeEffect;

	private void Start()
	{
		_LaserEffect.gameObject.SetActive(false);
		_chargeEffect.gameObject.SetActive(false);
		StartCoroutine(CoroutineFire());
	}

	private IEnumerator CoroutineFire()
	{
		while(true)
		{
			//Charge
			_chargeEffect.gameObject.SetActive(true);
			_chargeEffect.Play(true);
			yield return new WaitForSeconds(3f);
			_chargeEffect.gameObject.SetActive(false);

			//Laser
			_LaserEffect.SetActive(true);
			yield return new WaitForSeconds(_laserLifeTime);
			_LaserEffect.SetActive(false);
			yield return new WaitForSeconds(_delayTime);
		}
	}
}