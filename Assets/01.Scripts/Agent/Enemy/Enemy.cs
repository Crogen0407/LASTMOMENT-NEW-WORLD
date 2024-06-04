using Crogen.AgentFSM;
using UnityEngine;

public enum Direction
{
    right,
    left,
    up, 
    down
}

public class Enemy : Agent<EnemyStateEnum>
{
    //Managements
    private StageManager _stageManager;
    private CameraManager _cameraManager;
    
    public LayerMask whatIsPlayer;
    public float recognitionRange = 50f;
    public Transform currentTarget;
    public Direction preferredDirection;
    [SerializeField] private float _explosionRadius;

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
    }

    public override void SetDead()
    {
        _stageManager.DeCountEnemy(this);
        if (Physics.SphereCast(transform.position, recognitionRange, Vector3.up, out RaycastHit hit, whatIsPlayer))
        {
            _cameraManager.SetPlayerCameraShack(1, 10, 5);
        }
        Destroy(gameObject);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, recognitionRange);
    }
}