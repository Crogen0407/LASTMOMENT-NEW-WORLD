using UnityEngine.Rendering.Universal;

public class ScreenFadeRenderFeature : ScriptableRendererFeature
{
    private ScreenFadeRenderPass _renderPass = null;

    public override void Create()
    {
        _renderPass = new ScreenFadeRenderPass("ScreenFadePass", RenderPassEvent.AfterRenderingPostProcessing);
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        _renderPass.Setup(renderer.cameraColorTarget);
        renderer.EnqueuePass(_renderPass);
    }
}
