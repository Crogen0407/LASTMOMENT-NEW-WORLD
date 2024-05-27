using UnityEngine;

public class HighBuster : MonoBehaviour
{
    private Renderer[] _renderers;
    private Light[] _lights;
    
    private int _emissionID;
    
    private void Awake()
    {
        _renderers = transform.GetComponentsInChildren<Renderer>();
        _lights = transform.GetComponentsInChildren<Light>();
        _emissionID = Shader.PropertyToID("_EmissionColor");
    }

    public void SetColor(Color color)
    {
        for (int i = 0; i < _renderers.Length; ++i)
        {
            _renderers[i].material.SetColor(_emissionID, color);
        }
        for (int i = 0; i < _lights.Length; ++i)
        {
            _lights[i].color = color;
        }
    }
}
