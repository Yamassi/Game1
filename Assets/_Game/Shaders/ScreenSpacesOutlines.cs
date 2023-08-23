// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Rendering;
// using UnityEngine.Rendering.Universal;

// public class ScreenSpacesOutlines : ScriptableRendererFeature
// {
//     [System.Serializable]
//     private class ViewSpaceNormalsTextureSettings
//     {
//     }
//     private class ViewSpaceNormalsTexturePass : ScriptableRenderPass
//     {

//         private ViewSpaceNormalsTextureSettings _normalsTextureSettings;
//         private readonly RenderTargetHandle normals;
//         public ViewSpaceNormalsTexturePass(RenderPassEvent renderPassEvent)
//         {
//             this.renderPassEvent = renderPassEvent;
//             normals.Init("_SceneViewSpaceNormals");
//         }
//         public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
//         {
//             RenderTextureDescriptor normalsTextureDescriptor = cameraTextureDescriptor;
//             normalsTextureDescriptor.colorFormat = _normalsTextureSettings.colorFormat;
//             cameraTextureDescriptor.
//             cmd.GetTemporaryRT(normals.id, cameraTextureDescriptor, FilterMode.Point);
//         }
//         public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
//         {

//         }
//         public override void OnCameraCleanup(CommandBuffer cmd)
//         {
//             base.OnCameraCleanup(cmd);
//         }
//     }
//     [System.Serializable]
//     private class ScreenSpacesOutlinePassSettings
//     {
//     }
//     private class ScreenSpacesOutlinePass : ScriptableRenderPass
//     {
//         public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
//         {

//         }
//     }
//     [SerializeField] private RenderPassEvent _renderPassEvent;
//     private ViewSpaceNormalsTexturePass _viewSpaceNormalsTexturePass;
//     private ScreenSpacesOutlinePass _screenSpacesOutlinePass;
//     public override void Create()
//     {
//         _viewSpaceNormalsTexturePass = new ViewSpaceNormalsTexturePass(_renderPassEvent);
//         _screenSpacesOutlinePass = new ScreenSpacesOutlinePass(_renderPassEvent);
//     }
//     public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
//     {

//     }
// }
