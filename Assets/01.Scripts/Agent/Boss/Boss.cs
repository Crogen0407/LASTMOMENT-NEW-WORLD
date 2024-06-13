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
}
