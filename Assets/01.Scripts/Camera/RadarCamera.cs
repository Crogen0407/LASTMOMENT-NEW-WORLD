using UnityEngine;

public class RadarCamera : MonoBehaviour
{
    [SerializeField] private Transform _followTarget;
    [SerializeField] private float _height = 5000f;
    private void Awake()
    {
        transform.eulerAngles = Vector3.right * 90f;
    }

    private void FixedUpdate()
    {
        Vector3 pos = new Vector3(
            _followTarget.position.x,
            _followTarget.position.y + _height,
            _followTarget.position.z);
        transform.position = pos;
        transform.eulerAngles = new Vector3(90f, 0, 0);
    }
}
