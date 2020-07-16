using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode, ImageEffectAllowedInSceneView]
public class Fog : MonoBehaviour
{
    public Material material;
    public Color _FogColor;
    public float _DepthStart;
    public float _DepthDistance;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
    }

    // Update is called once per frame
    void Update()
    {
        material.SetColor("_FogColor", _FogColor);
        material.SetFloat("_DepthStart", _DepthStart);
        material.SetFloat("_DepthDistance", _DepthDistance);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, material);
    }
}
