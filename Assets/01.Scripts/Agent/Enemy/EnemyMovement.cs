using System.Collections;
using CurvedPathGenerator;
using DG.Tweening;
using UnityEngine;

public class EnemyMovement : AgentMovement
{
    public Transform attackTargetTrm;
    public float rotateDelay = 1f;
    private PathFollower _pathFollower;
    [SerializeField] private PathGenerator _pathGenerator;
    private bool _isRotating;

    protected override void Awake()
    {
        base.Awake();
        _pathFollower = GetComponent<PathFollower>();
    }

    public override void HandleMoveDirection(Vector3 Delta)
    {
        if (_isRotating == false)
        {
            Quaternion rot = Quaternion.LookRotation(Delta);
            transform.DORotateQuaternion(rot, rotateDelay).OnStart(() => _isRotating = true).OnComplete(() => _isRotating = false);
        }    
    }

    public override void HandleSpeedChange(bool value)
    {
        base.HandleSpeedChange(value);
        ExitDefaultBezierPath();
        Debug.Log(value);
        StartCoroutine(ChangeSpeedCoroutine());
    }

    private IEnumerator ChangeSpeedCoroutine()
    {
        float percentTime = 0;
        float currentTime = 0;
        float duration = 2f;
        while (percentTime < 1f)
        {
            yield return null;
            currentTime += Time.deltaTime;
            percentTime = currentTime / duration;
            CurSpeed = (int)(MaxSpeed * percentTime);
        }
        yield return new WaitForSeconds(duration);
        CurSpeed = MaxSpeed;
    }
    
    public void EnterDefaultBezierPath()
    {
        _pathFollower.Generator = _pathGenerator;
        _pathFollower.enabled = true;
    }

    public void ExitDefaultBezierPath()
    {
        _pathFollower.Generator = null;
        _pathFollower.enabled = false;
    }
}
