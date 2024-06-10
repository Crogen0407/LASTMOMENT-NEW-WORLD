using UnityEngine;

public class AutoAim : MonoBehaviour
{
    private Transform _playerCameraTrm;
    private MeshRenderer _meshRenderer;
    private float _dieDistance;
    [SerializeField] private bool _notDisable = false;
    private void Start()
    {
        _playerCameraTrm = GameObject.Find("PlayerVirtualCamera").transform;
        _meshRenderer = GetComponent<MeshRenderer>();
        Player player = FindObjectOfType<Player>();;
        _dieDistance = player.PlayerAttack.GetAttackRange();
    }

    private void FixedUpdate()
    {
        Vector3 dir = _playerCameraTrm.position - transform.position;
        _meshRenderer.enabled = dir.magnitude < _dieDistance || _notDisable;
        transform.LookAt(_playerCameraTrm, transform.up);
        
    }
}
