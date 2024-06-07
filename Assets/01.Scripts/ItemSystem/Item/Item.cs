using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    [SerializeField] private LayerMask _whatIsPlayer;
    [SerializeField] private ItemType _itemType;
    [SerializeField] private UnityEvent _disableEvent;
    private Collider[] _playerCollider;
    protected ItemManager _itemManager;
    
    protected void Awake()
    {
        _itemManager = ItemManager.Instance; 
        _playerCollider = new Collider[1];
    }

    private void FixedUpdate()
    {
        Physics.OverlapSphereNonAlloc(transform.position, 6f, _playerCollider, _whatIsPlayer);
        if (_playerCollider[0] != null)
        {
            if (_itemManager.PushInItemArray(_itemType))
            {
                _disableEvent?.Invoke();
                Destroy(gameObject);
            }
            else
            {
                _playerCollider[0] = null;
            }
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 6f);
    }
}
