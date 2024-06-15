using UnityEngine;
using UnityEngine.Serialization;

public class BossMovement : AgentMovement
{
    private Transform _playerTrm;
    [SerializeField] private float _rotateSpeed = 5f;
    protected override void Awake()
    {
        base.Awake();

        _playerTrm = GameManager.Instance.Player.transform;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        Vector3 newPos = new Vector3(_playerTrm.position.x, transform.position.y, _playerTrm.position.z);
        Quaternion lookRot = Quaternion.LookRotation(newPos - transform.position);

        transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, _rotateSpeed * (1/Quaternion.Angle(transform.rotation, lookRot)) * Time.fixedDeltaTime);

        CurSpeed = MaxSpeed;
    }

    public override void HandleMoveDirection(Vector3 Delta)
    {
        
    }
}
