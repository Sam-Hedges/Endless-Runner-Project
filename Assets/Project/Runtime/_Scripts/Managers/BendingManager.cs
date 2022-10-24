using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace Project.Runtime._Scripts.Managers
{
  [ExecuteAlways]
  public class BendingManager : MonoBehaviour
  {
    private const string BENDING_FEATURE = "ENABLE_BENDING";
    private const string PLANET_FEATURE = "ENABLE_BENDING_PLANET";
    private static readonly int BENDING_AMOUNT = Shader.PropertyToID("Bending Power");
    
    [SerializeField] private bool enablePlanet;
  
    [SerializeField] [Range(0f, 0.1f)] private float bendingAmount = 0.0018f;
    
    private float prevAmount;
  

    private void Awake() {
      
      if ( Application.isPlaying )
        Shader.EnableKeyword(BENDING_FEATURE);
      else
        Shader.DisableKeyword(BENDING_FEATURE);
  
      if ( enablePlanet )
        Shader.EnableKeyword(PLANET_FEATURE);
      else
        Shader.DisableKeyword(PLANET_FEATURE);
  
      UpdateBendingAmount();
    }
  
    private void OnEnable() {
      
      if ( !Application.isPlaying )
        return;
      
      RenderPipelineManager.beginCameraRendering += OnBeginCameraRendering;
      RenderPipelineManager.endCameraRendering += OnEndCameraRendering;
    }
  
    private void Update() {
      
      // Same as (prevAmount != bendingAmount) but corrected to stop
      // errors with rounding of small floating point values
      if (!Mathf.Approximately(prevAmount, bendingAmount)) {
        
      }
      UpdateBendingAmount();
    }
  
    private void OnDisable() {
      
      RenderPipelineManager.beginCameraRendering -= OnBeginCameraRendering;
      RenderPipelineManager.endCameraRendering -= OnEndCameraRendering;
    }
    
    private void UpdateBendingAmount() {
      
      prevAmount = bendingAmount;
      
      Shader.SetGlobalFloat(BENDING_AMOUNT, bendingAmount);
    }
  
    private static void OnBeginCameraRendering(ScriptableRenderContext ctx, Camera cam) {
      
      cam.cullingMatrix = 
        Matrix4x4.Ortho(-99, 99, -99, 99, 0.001f, 99) * cam.worldToCameraMatrix;
    }
  
    private static void OnEndCameraRendering(ScriptableRenderContext ctx, Camera cam) {
      
      cam.ResetCullingMatrix();
    }
  }
}