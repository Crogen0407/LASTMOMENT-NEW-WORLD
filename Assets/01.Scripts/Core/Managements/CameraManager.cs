using System.Collections;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoSingleton<CameraManager>
{
    private CinemachineVirtualCamera _playerVirtualCamera;
    private bool _isShacking;
    
    private void Awake()
    {
        _playerVirtualCamera = GameObject.Find("PlayerVirtualCamera").GetComponent<CinemachineVirtualCamera>();
    }

    public void SetPlayerCameraShack(float duration)
    {
        
    }

    private IEnumerator CoroutinePlayerCameraShack(float duration)
    {
        yield return new WaitForSeconds(duration);
        _playerVirtualCamera.GetCinemachineComponent<>()
    }
}
