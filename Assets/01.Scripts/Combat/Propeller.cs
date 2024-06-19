using UnityEngine;

public class Propeller : MonoBehaviour
{
    [SerializeField] private float _speed = 100f;

    private void Update()
    {
        transform.localEulerAngles += new Vector3(0, _speed * Time.deltaTime, 0);
    }
}
