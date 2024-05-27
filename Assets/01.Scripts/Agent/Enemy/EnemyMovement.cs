using CurvedPathGenerator;
using DG.Tweening;
using UnityEngine;

public class EnemyMovement : AgentMovement
{
    //[SerializeField] private List<Vector3> _bezierPointPositions;
    public float rotateDelay = 0.5f;
    private PathFollower _pathFollower;
    
    protected override void Awake()
    {
        base.Awake();
        _pathFollower = GetComponent<PathFollower>();
    }

    public override void HandleMoveDirection(Vector3 Delta)
    {
        Vector3 rotation = Quaternion.LookRotation(Delta).eulerAngles;
        transform.DORotate(rotation, rotateDelay);
    }

    private bool _isBezierPath;
    
    public void EnterDefaultBezierPath()
    {
        _isBezierPath = true;
    }

    public void ExitDefaultBezierPath()
    {
        _isBezierPath = false;
    }
    
    // private void OnDrawGizmosSelected()
    // {
    //     for (int i = 0; i < _bezierPointPositions.Count; ++i)
    //     {
    //         Gizmos.color = Color.blue;
    //         Gizmos.DrawSphere(_bezierPointPositions[i], 0.5f);
    //         if (i == _bezierPointPositions.Count - 1)
    //         {
    //             Gizmos.DrawLine(_bezierPointPositions[i], _bezierPointPositions[0]);
    //             continue;
    //         }
    //         Gizmos.DrawLine(_bezierPointPositions[i], _bezierPointPositions[i+1]);
    //         Gizmos.color = Color.white;
    //     }
    // }
}
