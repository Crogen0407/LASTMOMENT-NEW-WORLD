using Crogen.AgentFSM;
using UnityEngine;


public class Enemy : Agent<EnemyStateEnum>
{
    //Managements
    private StageManager _stageManager;
    private CameraManager _cameraManager;
    private ItemManager _itemManager;
    
    public LayerMask whatIsPlayer;
    public float recognitionRange = 50f;
    [SerializeField] private float _explosionRadius;

    //Item
    [SerializeField] private ItemType _droItemType;
    [Range(0, 100)] 
    [SerializeField] private float _itemDropPercent; 
        
    [Header("Attack")] 
    public PoolType bulletType;
    public float attackDelay = 0.1f;
    public int attackMaxCount = 10;
    public float attackLoadingDelay = 5f;
    private bool isAttacking = false;

    protected override void Awake()
    {
        base.Awake();
        _stageManager = StageManager.Instance;
        _cameraManager = CameraManager.Instance;
        _itemManager = ItemManager.Instance;

        //Item Percent Init
        float rangeValue = Random.Range(0, 100);
        if (rangeValue > _itemDropPercent)
        {
            _droItemType = ItemType.None;
        }
    }

    public override void SetDead()
    {
        _stageManager.DeCountEnemy(this);
        if (Physics.SphereCast(transform.position, recognitionRange, Vector3.up, out RaycastHit hit, whatIsPlayer))
        {
            _itemManager.DropItem(transform.position, _droItemType);
            _cameraManager.SetPlayerCameraShack(1, 10, 5);
        }
        Destroy(gameObject);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, recognitionRange);
    }
}