using Crogen.HealthSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public class Laser : MonoBehaviour
{
    [SerializeField] private LayerMask _hitTarget;
    [SerializeField] private Volume _volume;
	private Bloom _bloom;
	private float _bloomDefaultIntensity;
	private readonly float _maxBloomDefaultInstensity = 10000f;
	private bool _isBloomValueChanging = false;

	private void Awake()
	{
		_volume = FindObjectOfType<Volume>(false);
		_volume.profile.TryGet<Bloom>(out _bloom);
		_bloomDefaultIntensity = _bloom.intensity.value;
	}

	private void OnEnable()
	{
		SoundManager.Instance.PlaySFX(AudioType.SFX_ExploisonNoise, transform.position);
	}

	void FixedUpdate()
    {
        RaycastHit[] hits = new RaycastHit[1];
		Vector3 tf = transform.forward.normalized * 2000;
		ChangeIntensity(
			Physics.CapsuleCastNonAlloc(transform.position, 
			transform.position + tf, 
			15, Vector3.forward, hits, 0f, _hitTarget) > 0);
	}

	private void ChangeIntensity(bool isTooCloseTarget)
	{
		if (_isBloomValueChanging) return;
		float duration = 0.5f;
		_isBloomValueChanging = true;
		DOTween.To(
			() => _bloom.intensity.value, 
			(float x) => _bloom.intensity.value = x,
			isTooCloseTarget ? _maxBloomDefaultInstensity : _bloomDefaultIntensity, 
			duration).OnKill(() => _isBloomValueChanging = false);
	}
}
