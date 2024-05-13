using UnityEngine;


public class NutrientManager : MonoBehaviour
{
    public ComputeShader nutrientComputeShader;
    public RenderTexture nutrientTexture;
    public int textureResolution = 128;
    //public float globalSoakRate = 0.1f;
    //public float globalDepletionRate = 0.05f;

    private RenderTexture renderTexture;
    //private int kernelHandle;

    private void Start()
    {
    //    kernelHandle = nutrientComputeShader.FindKernel("NutrientUpdate");

        // Modify the creation of nutrientTexture to include the RenderTextureReadWrite.Linear flag
        nutrientTexture = new RenderTexture(textureResolution, textureResolution, 0, RenderTextureFormat.RFloat, RenderTextureReadWrite.Linear);
        nutrientTexture.enableRandomWrite = true;
        nutrientTexture.Create();

        nutrientComputeShader.SetTexture(0, "NutrientTexture", nutrientTexture);
        nutrientComputeShader.Dispatch(0, textureResolution / 8, textureResolution / 8, 1);
    }

    private void Update()
    {
    //    nutrientComputeShader.SetFloat("SoakRate", globalSoakRate * Time.deltaTime);
    //    nutrientComputeShader.SetFloat("DepletionRate", globalDepletionRate * Time.deltaTime);
    //    nutrientComputeShader.SetTexture(kernelHandle, "NutrientTexture", nutrientTexture);

        // Dispatch compute shader
    //    nutrientComputeShader.Dispatch(kernelHandle, textureResolution / 8, textureResolution / 8, 1);
        //print("dispatched");
    }
}
