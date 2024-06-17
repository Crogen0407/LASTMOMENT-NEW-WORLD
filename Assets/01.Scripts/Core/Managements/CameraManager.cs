using System.Collections;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class CameraManager : MonoSingleton<CameraManager>
{
    [SerializeField] private CinemachineVirtualCamera _playerVirtualCamera;
    private CinemachineBasicMultiChannelPerlin _perlin;
    private bool _isShacking;
    
    private void Awake()
    {
        _perlin = _playerVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void FadePlayerCameraShack(float duration, float amplitude, float frequency)
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(DOTween.To(x => _perlin.m_AmplitudeGain = x, amplitude, 0f, duration));
        seq.Join(DOTween.To(x => _perlin.m_FrequencyGain = x, frequency, 0f, duration));
    }

    public void SetPlayerCameraShack(float duration, float amplitude, float frequency)
    {
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
