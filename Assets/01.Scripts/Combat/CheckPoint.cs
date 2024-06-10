using System;
using UnityEngine;
using UnityEngine.Events;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private LayerMask _whatIsPlayer;
    [SerializeField] private float _radius=40f;
    [Range(0f, 1f)] [SerializeField] private float clearGauge;
    [SerializeField] private UnityEvent _clearEvent;
    private Collider[] _colliders;

    private void Awake()
    {
        _colliders = new Collider[1];
    }
                 
    public void AddClearGauge(float value)
    {
        clearGauge += value;
    }
    
    private void FixedUpdate()
    {
        if (Physics.OverlapSphereNonAlloc(transform.position, _radius, _colliders, _whatIsPlayer) > 0 && clearGauge >= 1f)
        {
            _clearEvent?.Invoke();
            StageManager.Instance.UpdateCurrentCheckPoint();            
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _radius);
        Gizmos.color = Color.white;
    }
}
