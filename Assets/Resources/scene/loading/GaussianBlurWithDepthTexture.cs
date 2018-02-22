using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaussianBlurWithDepthTexture : PostEffectsBase {

    public Shader gaussianBlurShader;
    private Material gaussianBlurMaterial = null;

    public Material material {
        get {
            gaussianBlurMaterial = CheckShaderAndCreateMaterial(gaussianBlurShader, gaussianBlurMaterial);
            return gaussianBlurMaterial;
        }
    }

    // Blur iterations - larger number means more blur.
    [Range(0, 32)]
    public int iterations = 6;

    // Blur spread for each iteration - larger value means more blur
    [Range(0.0f, 30.0f)]
    public float blurSpread = 3.0f;

    [Range(1, 8)]
    public int downSample = 1;

    // 焦距
    [Range(0, 30)]
    public float focus = 1;
    
    // 景深
    [Range(0, 5)]
    public float depthOfField = 1;

    // 过渡距离
    [Range(0, 10)]
    public float transition = 2;

    // Use this for initialization
    void Start() {
        // GetComponent<Camera>().depthTextureMode |= DepthTextureMode.DepthNormals;
    }

    void OnEnable() {
		GetComponent<Camera>().depthTextureMode |= DepthTextureMode.Depth;
	}

    // Update is called once per frame
    void Update() {

    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination) {
        if (material != null) {
            RenderTexture buffer0 = RenderTexture.GetTemporary(source.width, source.height, 0);
            Graphics.Blit(source, buffer0);

            for (int i = 0; i < iterations; i++) {
                material.SetFloat("_BlurSize", i * blurSpread);
                material.SetFloat("_focus", focus);
                material.SetFloat("_depthOfField", depthOfField);
                material.SetFloat("_transition", transition);

                RenderTexture buffer1 = RenderTexture.GetTemporary(source.width, source.height, 0);
                Graphics.Blit(buffer0, buffer1, material, 0);
                RenderTexture.ReleaseTemporary(buffer0);
                buffer0 = buffer1;
                buffer1 = RenderTexture.GetTemporary(source.width, source.height, 0);
                Graphics.Blit(buffer0, buffer1, material, 1);
                RenderTexture.ReleaseTemporary(buffer0);
                buffer0 = buffer1;
                
            }

            Graphics.Blit(buffer0, destination);
            RenderTexture.ReleaseTemporary(buffer0);
        } else {
            Graphics.Blit(source, destination);
        }
    }
}
