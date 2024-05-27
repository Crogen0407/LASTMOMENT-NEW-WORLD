using CurvedPathGenerator;
using DG.Tweening;
using UnityEngine;

public class EnemyMovement : AgentMovement
{
    //[SerializeField] private List<Vector3> _bezierPointPositions;
    public float rotateDelay = 0.5f;
    private PathFollower _pathFollower;
    private bool _isBezierPath;
    
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

    public void EnterDefaultBezierPath()
    {
        _isBezierPath = true;
    }

    public void ExitDefaultBezierPath()
    {
        _isBezierPath = false;
    }
}
