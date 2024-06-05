using UnityEngine;

public abstract class Item : MonoPoolingObject
{
    [SerializeField] private LayerMask _whatIsPlayer;
    [SerializeField] private ItemType _itemType;
    
    private Collider[] _playerCollider = new Collider[1];

    private void FixedUpdate()
    {
        Physics.OverlapSphereNonAlloc(transform.position, 6f, _playerCollider, _whatIsPlayer);
        if (_playerCollider[0] != null)
        {
            _playerCollider[0].GetComponent<ItemManager>().PushInItemArray(_itemType);
            Destroy(gameObject);
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 6f);
    }
}
