using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[System.Serializable]
public class MultiPassRendererFeature : ScriptableRendererFeature
{
    public List<string> lightModePasses;
    private MultiRenderPass mainPass;
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderer.EnqueuePass(mainPass);
    }
    public override void Create()
    {
        mainPass = new MultiRenderPass(lightModePasses);
    }
}
