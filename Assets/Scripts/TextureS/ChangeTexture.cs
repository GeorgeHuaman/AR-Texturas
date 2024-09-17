using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTexture : MonoBehaviour
{
    public ObjectManipulator objectManipulator;
    public Material quadMaterial;
    public List<MainTexture> mainTextures;

    [SerializeField]private int currentMainTextureIndex = 0; // Índice de la textura principal actual
    [SerializeField]private int currentVariantIndex = 0;     // Índice de la variante actual

    private bool once;

    private void Update()
    {
        if (objectManipulator.arObject != null && !once)
        {
            quadMaterial = objectManipulator.arObject.GetComponent<Renderer>().material;
            once = true;

            ApplyMainTexture();
        }
    }

    public void ChangeMainTexture()
    {
        if (quadMaterial != null && mainTextures.Count > 0)
        {
            currentMainTextureIndex = (currentMainTextureIndex + 1) % mainTextures.Count;
            currentVariantIndex = 0;
            ApplyMainTexture();
        }
    }

    public void ChangeVariantTexture()
    {
        if (quadMaterial != null && mainTextures[currentMainTextureIndex].variants.Count > 0)
        {
            currentVariantIndex = (currentVariantIndex + 1) % mainTextures[currentMainTextureIndex].variants.Count;
            ApplyVariantTexture();
        }
    }

    // Método para aplicar la textura principal o variante actual
    private void ApplyMainTexture()
    {
        quadMaterial.mainTexture = mainTextures[currentMainTextureIndex].mainTexture;
    }

    private void ApplyVariantTexture()
    {
        if (mainTextures[currentMainTextureIndex].variants.Count > 0)
        {
            quadMaterial.mainTexture = mainTextures[currentMainTextureIndex].variants[currentVariantIndex];
        }
    }

}

[System.Serializable]
public class MainTexture
{
    public Texture mainTexture;
    public List<Texture> variants = new List<Texture>();
}
