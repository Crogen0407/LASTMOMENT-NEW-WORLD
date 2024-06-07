using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[Serializable, VolumeComponentMenuForRenderPipeline("Crogen/ScreenFade", typeof(UniversalRenderPipeline))]
public class ScreenFade : VolumeComponent, IPostProcessComponent
{
    //Shader
    private const string _shaderPath = "Custom/ScreenFadeShader";
    private const string _property_amount = "_Amount";
    private Material _material;
    
    public ClampedFloatParameter amount = new ClampedFloatParameter(0f, 0f, 1f);
    
    public bool IsActive()
    {
        if (!active || !_material || amount==1) return false;
        return true;
    }

    public void Setup()
    {
        if (!_material)
        {
            Shader shader = Shader.Find(_shaderPath);
            _material = CoreUtils.CreateEngineMaterial(shader);
        }
    }
    
    public bool IsTileCompatible() => false;

    public void Destroy()
    {
        if (_material)
        {
            CoreUtils.Destroy(_material);
            _material = null;
        }
    }
    
    public void Render(CommandBuffer commandBuffer, ref RenderingData renderingData, RenderTargetIdentifier source, RenderTargetIdentifier destination)
    {
        if (!_material) return;

        _material.SetFloat(_property_amount, amount.value);

        commandBuffer.Blit(source, destination, _material);
    }
}
