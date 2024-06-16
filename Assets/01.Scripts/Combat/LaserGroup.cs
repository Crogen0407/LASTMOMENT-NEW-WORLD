using UnityEngine;

public class LaserGroup : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed = 5;
    [SerializeField] private GameObject[] _lasers;
    
    public void SetLaserGroup(float rotateSpeed)
    {
        _rotateSpeed = rotateSpeed;
    }

    public void SetLaserActive(bool active)
    {
        foreach (var t in _lasers)
            t.SetActive(active);
    }
    
    private void FixedUpdate()
    {
        transform.Rotate(Vector3.forward * _rotateSpeed, Space.Self);
    }
}