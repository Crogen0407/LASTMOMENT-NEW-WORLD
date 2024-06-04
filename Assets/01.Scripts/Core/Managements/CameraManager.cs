using System.Collections;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoSingleton<CameraManager>
{
    private CinemachineVirtualCamera _playerVirtualCamera;
    private CinemachineBasicMultiChannelPerlin _perlin;
    private bool _isShacking;
    private float amplitude;
    private float frequency;
    private void Awake()
    {
        _playerVirtualCamera = GameObject.Find("PlayerVirtualCamera").GetComponent<CinemachineVirtualCamera>();
        _perlin = _playerVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void SetPlayerCameraShack(float duration, float amplitude, float frequency)
    {
        if (_isShacking) return;
        StartCoroutine(CoroutinePlayerCameraShack(duration, amplitude, frequency));
    }

    private IEnumerator CoroutinePlayerCameraShack(float duration, float amplitude, float frequency)
    {
        _isShacking = true;
        _perlin.m_AmplitudeGain += this.amplitude;
        _perlin.m_FrequencyGain += this.frequency;
        yield return new WaitForSeconds(duration);
        _isShacking = false;
        _perlin.m_AmplitudeGain -= this.amplitude;
        _perlin.m_FrequencyGain -= this.frequency;
    }
}
