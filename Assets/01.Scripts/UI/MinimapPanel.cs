using UnityEngine;

public class MinimapPanel : MonoBehaviour
{
    [SerializeField] private Material _radarMat;
    private int _radarShaderHash;
    private float _currentTime = 0;
    [SerializeField] private float _updateTime = 1f;
    private Transform _playerTrm;
    private Transform _gridTrm;
    
    private void Awake()
    {
        _playerTrm = FindObjectOfType<Player>().transform;
        _gridTrm = transform.Find("Grid");
        _radarShaderHash = Shader.PropertyToID("_Value");
    }

    private void Update()
    {
        _currentTime += Time.deltaTime * (1/_updateTime);
        if (_currentTime > 1)
        {
            _currentTime = 0;
        }
        _radarMat.SetFloat(_radarShaderHash, _currentTime);
        RotateMinimap(_playerTrm.eulerAngles.y);
    }

    private void RotateMinimap(float rotate)
    {
        _gridTrm.eulerAngles = Vector3.forward * rotate;
    }
}