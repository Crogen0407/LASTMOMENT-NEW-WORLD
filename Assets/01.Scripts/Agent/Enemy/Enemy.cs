using Crogen.AgentFSM;
using UnityEngine;

public class Enemy : Agent<EnemyStateEnum>
{
    public LayerMask whatIsPlayer;
    public float recognitionRange = 50f;
    public Transform currentTarget;

    public override void SetDead()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, recognitionRange);
        Gizmos.color = Color.white;
    }
}