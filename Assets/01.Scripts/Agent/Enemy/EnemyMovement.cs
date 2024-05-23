using DG.Tweening;
using UnityEngine;

public class EnemyMovement : AgentMovement
{
    public float rotateDelay = 0.5f;
    
    public override void HandleMoveDirection(Vector3 Delta)
    {
        Vector3 rotation = Quaternion.LookRotation(Delta).eulerAngles;
        transform.DORotate(rotation, rotateDelay);
    }
}
