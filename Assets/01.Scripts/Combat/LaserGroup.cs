using UnityEngine;

public class LaserGroup : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed = 5;
    private LaserShooter[] _lasers;

    public void SetLaserGroup(float rotateSpeed)
    {
        _rotateSpeed = rotateSpeed;
    }
    
    public void ChargeEffectActive(bool active)
    {
        _lasers ??= GetComponentsInChildren<LaserShooter>();

        for (int i = 0; i < _lasers.Length; ++i)
        {
            _lasers[i].ChargeEffectActive(active);
        }
    }

    public void FadeLaserEffect(float fadeTime = 1f, float duration = 2f)
    {
        _lasers ??= GetComponentsInChildren<LaserShooter>();

        for (int i = 0; i < _lasers.Length; ++i)
        {
            _lasers[i].FadeLaserEffect(fadeTime, duration);
        }
        
    }
    
    public void SetLaserActive(bool active)
    {
        _lasers ??= GetComponentsInChildren<LaserShooter>();

        for (int i = 0; i < _lasers.Length; ++i)
        {
            _lasers[i].SetLaserEffectActive(active);
        }
    }
    
    private void FixedUpdate()
    {
        transform.Rotate(Vector3.forward * _rotateSpeed, Space.Self);
    }
}