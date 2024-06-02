using UnityEngine;

public class Gear : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    private Vector3 _rotateAxis;

    private void Awake()
    {
        _rotateAxis = new Vector3(
            Random.Range(-1, 1),
            Random.Range(-1, 1),
            Random.Range(-1, 1));

        _rotateAxis = _rotateAxis.normalized;
    }

    void FixedUpdate()
    {
        transform.Rotate(_rotateAxis * _speed * Time.fixedDeltaTime, Space.World);
    }
}
