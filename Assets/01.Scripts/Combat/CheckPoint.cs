using System;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private LayerMask _whatIsPlayer;
    [SerializeField] private float _radius=40f;
    [Range(0f, 1f)] public float clearGauge = 1f;
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
