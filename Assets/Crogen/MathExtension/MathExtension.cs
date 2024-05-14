using UnityEngine;

public class MathExtension : MonoBehaviour
{
    public static float RotateClamp(float value, float min, float max)
    {
        float angle = value > 180f ? value - 360f : value;

        angle = Mathf.Clamp(value, min, max);

        return angle;
    }
}
