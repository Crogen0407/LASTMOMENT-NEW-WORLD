using UnityEngine;

public class AutoAim : MonoBehaviour
{
    private Transform _playerCameraTrm;
    private MeshRenderer _meshRenderer;
    private float _dieDistance;
    
    private void Start()
    {
        _playerCameraTrm = GameObject.Find("PlayerVirtualCamera").transform;
        _meshRenderer = GetComponent<MeshRenderer>();
        Player player;
        player = FindObjectOfType<Player>();
        _dieDistance = player.PlayerAttack.GetAttackRange();
    }

    private void FixedUpdate()
    {
        Vector3 dir = _playerCameraTrm.position - transform.position;
        _meshRenderer.enabled = dir.magnitude < _dieDistance;
        transform.forward = -dir;
    }
}
