using System;
using Crogen.AgentFSM;
using UnityEngine;

public class Boss : Agent<BossStateEnum>
{
    //Managements
    private StageManager _stageManager;
    private UIManager _uiManager;  
    
    public LayerMask whatIsPlayer;
    public float recognitionRange = 50f;
    
    public EnemyType enemyType;

    public BossAttack bossAttack;
    public EnemySpawner enemySpawner;

    protected override void Awake()
    {
        base.Awake();
        bossAttack = GetComponent<BossAttack>();
        enemySpawner = GetComponent<EnemySpawner>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, recognitionRange);
    }
}