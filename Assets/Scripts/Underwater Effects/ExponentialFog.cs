using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode, ImageEffectAllowedInSceneView]
public class ExponentialFog : MonoBehaviour
{
    public Material material;
    public Color _FogColor;

    [Range (0, 1)]
    public float _Density = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
    }

    // Update is called once per frame
    void Update()
    {
        material.SetColor("_FogColor", _FogColor);
        material.SetFloat("_Density", _Density);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, material);
    }
}
