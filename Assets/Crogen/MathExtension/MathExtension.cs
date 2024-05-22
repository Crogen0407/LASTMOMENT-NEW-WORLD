using UnityEngine;

public class MathExtension : MonoBehaviour
{
    public static float RotateClamp(float value, float min, float max)
    {
        float angle = value > 180f ? value - 360f : value;

        angle = Mathf.Clamp(value, min, max);

        return angle;
    }
    
    public static float PowerByTwo(float x) 
    {
        return x * x;
    }
    
    public static float Remap(float value, float inputMin, float inputMax, float outputMin, float outputMax)
    {
        return outputMin + (value - inputMin) * (outputMax - outputMin) / (inputMax - inputMin);
    }
}
