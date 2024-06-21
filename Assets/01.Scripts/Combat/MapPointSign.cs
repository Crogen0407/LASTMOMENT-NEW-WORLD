using UnityEngine;

public class MapPointSign : MonoBehaviour
{
    private Transform _radarCam;
    private float _camSize;
    [SerializeField] private Transform _mapEnablePoint;
    
    private void Awake()
    {
        _radarCam = GameObject.Find("RadarCamera").transform;
        _camSize = _radarCam.GetComponent<Camera>().orthographicSize-50;
    }

    private void OnValidate()
    {
        if (!transform.parent) return;
        if (transform.parent.TryGetComponent(out Enemy enemy))
        {
            transform.localScale = Vector3.one * enemy.recognitionRange;
        }
    }

    private void Update()
    {
        transform.forward = -Vector3.up;

        Vector3 camPos = new Vector3(_radarCam.position.x, _mapEnablePoint.position.y, _radarCam.position.z);
        Vector3 selfPos =  _mapEnablePoint.position;
        
        if (Vector3.Distance(selfPos, camPos) > _camSize)
        {
            selfPos = ((selfPos - camPos).normalized * _camSize) + camPos;
        }
        else
        {
            selfPos = transform.position;
        }
        _mapEnablePoint.position = selfPos;
    }
}
