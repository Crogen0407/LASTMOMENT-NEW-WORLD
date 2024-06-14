using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private LayerMask _whatIsPlayer;
    [SerializeField] private float _radius=40f;
    [Range(0f, 1f)] [SerializeField] private float _clearGauge;
    [SerializeField] private bool _isOnlyGaugeCanClear;
    [SerializeField] private UnityEvent _clearEvent;
    private Collider[] _colliders;

    private void Awake()
    {
        _colliders = new Collider[1];
    }
                 
    public void AddClearGauge(float value)
    {
        _clearGauge += value;
        if (_clearGauge >= 1f && _isOnlyGaugeCanClear)
        {
            _clearEvent?.Invoke();
            StageManager.Instance.UpdateCurrentCheckPoint();            
        }
    }
    
    private void FixedUpdate()
    {
        if (_isOnlyGaugeCanClear) return;
        if (Physics.OverlapSphereNonAlloc(transform.position, _radius, _colliders, _whatIsPlayer) > 0 && _clearGauge >= 1f)
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
