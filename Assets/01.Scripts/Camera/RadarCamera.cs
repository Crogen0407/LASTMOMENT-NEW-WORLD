using UnityEngine;

public class RadarCamera : MonoBehaviour
{
    [SerializeField] private Transform _followTarget;

    private void Awake()
    {
        transform.eulerAngles = Vector3.right * 90f;
    }

    private void FixedUpdate()
    {
        Vector3 pos = new Vector3(
            _followTarget.position.x,
            _followTarget.position.y + 500f,
            _followTarget.position.z);
        transform.position = pos;
        transform.eulerAngles = new Vector3(90f, _followTarget.eulerAngles.y, _followTarget.eulerAngles.z);
    }
}
