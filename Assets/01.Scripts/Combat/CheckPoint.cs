using UnityEngine;
using UnityEngine.Events;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private LayerMask _whatIsPlayer;
    [SerializeField] private float _radius=40f;
    [Range(0f, 1f)] [SerializeField] private float _clearGauge;
    [SerializeField] private bool _isOnlyGaugeCanClear;
    [SerializeField] private UnityEvent _clearEvent;
    private Collider[] _colliders;
    private bool _isClear = false;

    private void Awake()
    {
        _colliders = new Collider[1];
    }

    public void AddClearGauge(float value)
    {
        if (_isClear) return; 
        _clearGauge += value;
        if (_clearGauge >= 1f && _isOnlyGaugeCanClear)
        {
            OnClear();
        }
    }
    
    private void FixedUpdate()
    {
        if (_isClear) return; 
        if (_isOnlyGaugeCanClear) return;
        if (Physics.OverlapSphereNonAlloc(transform.position, _radius, _colliders, _whatIsPlayer) > 0 && _clearGauge >= 1f)
        {
            OnClear();
        }
    }

    private void OnClear()
    {
        Debug.Log("Clear"); 
        _isClear = true; 
        _clearEvent?.Invoke();
        StageManager.Instance.UpdateCurrentCheckPoint();      
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _radius);
        Gizmos.color = Color.white;
    }
}
