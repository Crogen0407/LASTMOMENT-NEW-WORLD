using System;
using UnityEngine;

public class MapPointSign : MonoBehaviour
{
    private void OnValidate()
    {
        if (!transform.parent) return;
        if (transform.parent.TryGetComponent(out Enemy enemy))
        {
            transform.localScale = Vector3.one * enemy.recognitionRange;
        }
    }

    private void FixedUpdate()
    {
        transform.forward = -Vector3.up;
    }
}
