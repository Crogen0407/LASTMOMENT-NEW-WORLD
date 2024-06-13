using System.Collections;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoSingleton<CameraManager>
{
    private CinemachineVirtualCamera _playerVirtualCamera;
    private CinemachineBasicMultiChannelPerlin _perlin;
    private bool _isShacking;
    
    private void Awake()
    {
        _playerVirtualCamera = GameObject.Find("PlayerVirtualCamera").GetComponent<CinemachineVirtualCamera>();
        _perlin = _playerVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void SetPlayerCameraShack(float duration, float amplitude, float frequency)
    {
        Debug.Log(amplitude);
        StartCoroutine(CoroutinePlayerCameraShack(duration, amplitude, frequency));
    }

    private IEnumerator CoroutinePlayerCameraShack(float duration, float amplitude, float frequency)
    {
        _perlin.m_AmplitudeGain += amplitude;
        _perlin.m_FrequencyGain += frequency;
        yield return new WaitForSeconds(duration);
        _perlin.m_AmplitudeGain -= amplitude;
        _perlin.m_FrequencyGain -= frequency;
    }
}
