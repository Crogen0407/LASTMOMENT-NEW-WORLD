using System.Collections;
using Crogen.AgentFSM;
using Crogen.ObjectPooling;
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
    
    public LayerMask whatIsPlayer;
    public float recognitionRange = 50f;
    public Transform currentTarget;
    public Direction preferredDirection;
    [SerializeField]private int findNavAngle;

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
    }

    public override void SetDead()
    {
        _stageManager.DeCountEnemy(this);
        Destroy(gameObject);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, recognitionRange);
    }
}