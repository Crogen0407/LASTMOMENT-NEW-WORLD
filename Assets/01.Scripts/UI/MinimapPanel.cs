using UnityEngine;

public class MinimapPanel : MonoBehaviour
{
    [SerializeField] private Material _radarMat;
    [SerializeField] private GameObject _radarCamObject;  
    private int _radarShaderHash;
    private float _currentTime = 0;
    [SerializeField] private float _updateTime = 1f;
    private void Awake()
    {
        _radarShaderHash = Shader.PropertyToID("_Value");
    }

    private void Update()
    {
        _radarCamObject.SetActive(false);
        _currentTime += Time.deltaTime * (1/_updateTime);
        if (_currentTime > 1)
        {
            _currentTime = 0;
            //약간 끊기는 업데이트를 주기 위해서
            _radarCamObject.SetActive(true);
        }
        _radarMat.SetFloat(_radarShaderHash, _currentTime);
    }
}