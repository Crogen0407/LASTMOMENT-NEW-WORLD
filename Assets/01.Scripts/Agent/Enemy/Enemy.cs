using Crogen.AgentFSM;
using UnityEngine;

public class Enemy : Agent<EnemyStateEnum>
{
    //Managements
    private CameraManager _cameraManager;
    private ItemManager _itemManager;
    
    public LayerMask whatIsPlayer;
    public float recognitionRange = 50f;
    [SerializeField] private float _explosionRadius;
    public EnemyType enemyType;

    //Item
    [SerializeField] private ItemType _dropItemType;
    [Range(0, 100)] 
    [SerializeField] private float _itemDropPercent;
    
    protected override void Awake()
    {
        base.Awake();
        _cameraManager = CameraManager.Instance;
        _itemManager = ItemManager.Instance;

        //Item Percent Init
        float rangeValue = Random.Range(0, 100);
        if (rangeValue > _itemDropPercent)
        {
            _dropItemType = ItemType.None;
        }
    }

    public override void SetDead()
    {
        _itemManager.DropItem(transform.position, _dropItemType);
        if (Physics.SphereCast(transform.position, recognitionRange, Vector3.up, out RaycastHit hit, whatIsPlayer))
        {
            Debug.Log("Die");
            _cameraManager.SetPlayerCameraShack(1, 10, 5);
        }
        base.SetDead();
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, recognitionRange);
    }
}