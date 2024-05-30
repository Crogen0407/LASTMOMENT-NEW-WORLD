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
        // 뒤를 제외한 방향에 총 13개의 ray를 쏴서 도망갈 수 있는 방향을 구하고
        // 도망갈 수 있는 방향 중 가장 플레이어랑 가까운 방향으로 이동

        for (int i = 0; i < 5; ++i)
        {
            Physics.Raycast(transform.position, new Vector3(Mathf.Cos((360 / 5)*Mathf.Deg2Rad), Mathf.Sin((360 / 5)*Mathf.Deg2Rad), recognitionRange));
            Debug.DrawRay(transform.position, transform.forward * recognitionRange);    
        }
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, recognitionRange);
        Gizmos.color = Color.white;
    }
}