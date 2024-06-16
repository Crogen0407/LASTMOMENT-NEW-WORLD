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
        if (_lasers == null)
        {
            _lasers = GetComponentsInChildren<LaserShooter>();
        }
        
        for (int i = 0; i < _lasers.Length; ++i)
        {
            _lasers[i].ChargeEffectActive(active);
        }
    }
    
    public void SetLaserActive(bool active)
    {
        if (_lasers == null)
        {
            _lasers = GetComponentsInChildren<LaserShooter>();
        }
        
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