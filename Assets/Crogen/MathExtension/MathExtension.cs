using UnityEngine;

public class MathExtension
{
    public static float RotateClamp(float value, float min, float max)
    {
        float angle = value > 180f ? value - 360f : value;

        angle = Mathf.Clamp(value, min, max);

        return angle;
    }

    public static Vector3 VectorClamp(Vector3 value, Vector3 min, Vector3 max)
    {
        for (short i = 0; i < 3; ++i)
        {
            if (value[i] > max[i])
            {
                value[i] = max[i];
            }
            if (value[i] < min[i])
            {
                value[i] = min[i];
            }
        }
        return value;
    }
    
    public static float PowerByTwo(float x) 
    {
        return x * x;
    }
    
    public static float Remap(float value, float inputMin, float inputMax, float outputMin, float outputMax)
    {
        return outputMin + (value - inputMin) * (outputMax - outputMin) / (inputMax - inputMin);
    }

    public static Vector3 ClampMagnitude(Vector3 vec, float maxValue)
    {
        if (vec.magnitude > maxValue)
            vec = vec/vec.magnitude*maxValue;
        return vec;
    }
}
