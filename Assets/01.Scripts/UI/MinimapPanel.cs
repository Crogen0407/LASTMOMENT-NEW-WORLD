using UnityEngine;

public class MinimapPanel : MonoBehaviour
{
    [SerializeField] private Material _radarMat;
    [SerializeField] private GameObject _radarCamObject;  
    private int _radarShaderHash;
    private float _currentTime = 0;
    
    private void Awake()
    {
        _radarShaderHash = Shader.PropertyToID("_Value");
    }

    private void Update()
    {
        _radarCamObject.SetActive(false);
        _currentTime += Time.deltaTime;
        if (_currentTime > 1f)
        {
            _currentTime = 0;
            //약간 끊기는 업데이트를 주기 위해서
            _radarCamObject.SetActive(true);
        }
        _radarMat.SetFloat(_radarShaderHash, _currentTime);
    }
}