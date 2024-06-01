using UnityEngine;

public class Item : MonoPoolingObject
{
    [SerializeField] private float _getableRadius;
    [SerializeField] private LayerMask _whatIsPlayer;
    [Tooltip("나중에 풀링타입 다 만들기")]
    [SerializeField] private PoolType _itemPoolingTye;
    [SerializeField] private ItemType _itemType;
    
    private Collider[] _playerCollider = new Collider[1];

    public override void OnPop()
    {
        
    }

    private void FixedUpdate()
    {
        Physics.OverlapSphereNonAlloc(transform.position, _getableRadius, _playerCollider, _whatIsPlayer);
        if (_playerCollider[0] != null)
        {
            Push(_itemPoolingTye);
        }
    }
    
    public override void OnPush()
    {
        _playerCollider[0].GetComponent<ItemManager>().PushInItemArray(_itemType);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _getableRadius);
    }
}
