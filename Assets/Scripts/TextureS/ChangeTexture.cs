using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTexture : MonoBehaviour
{
    public ObjectManipulator objectManipulator;
    public Renderer quadRenderer;
    public Renderer quadRendererQr;
    public List<MainMaterial> mainMaterials;

    [SerializeField] private int currentMainMaterialIndex = 0;
    [SerializeField] private int currentVariantIndex = 0; 

    private bool once;
    private bool onceQR;

    public Image mainTextures;
    public Image variantTextures;

    private void Update()
    {
        if (Manager.Instance.arObject != null && !once)
        {
            quadRenderer = Manager.Instance.arObject.GetComponent<Renderer>();
            once = true;

            ApplyMainMaterial();
        }
        if (Manager.Instance.arObjectQR != null && !onceQR)
        {
            quadRendererQr = Manager.Instance.arObjectQR.GetComponent<Renderer>();
            onceQR = true;

            ApplyMainMaterial();
        }
    }

    public void ChangeMainMaterial()
    {
        if (mainMaterials.Count > 0)
        {
            if (quadRenderer != null || quadRendererQr != null)
            {
                currentMainMaterialIndex = (currentMainMaterialIndex + 1) % mainMaterials.Count;
                currentVariantIndex = 0;
                ApplyMainMaterial();
                UpdateMainTextureUI();
            }
                
        }
    }

    public void ChangeVariantMaterial()
    {
        if (mainMaterials.Count > 0 && mainMaterials[currentMainMaterialIndex].variants.Count > 0)
        {
            if (quadRenderer != null || quadRendererQr != null)
            {
                currentVariantIndex = (currentVariantIndex + 1) % mainMaterials[currentMainMaterialIndex].variants.Count;
                ApplyVariantMaterial();
                UpdateVariantTextureUI();
            }
            
        }
    }

    // Método para aplicar el material principal actual
    private void ApplyMainMaterial()
    {
        if (quadRenderer != null)
        {
            quadRenderer.material = mainMaterials[currentMainMaterialIndex].mainMaterial;
        }
        if (quadRendererQr != null)
        {
            quadRendererQr.material = mainMaterials[currentMainMaterialIndex].mainMaterial;
        }
        UpdateVariantTextureUI();
    }

    // Método para aplicar la variante de material actual
    private void ApplyVariantMaterial()
    {
        if (quadRenderer != null && mainMaterials[currentMainMaterialIndex].variants.Count > 0)
        {
            quadRenderer.material = mainMaterials[currentMainMaterialIndex].variants[currentVariantIndex];
        }
        if (quadRendererQr != null && mainMaterials[currentMainMaterialIndex].variants.Count > 0)
        {
            quadRendererQr.material = mainMaterials[currentMainMaterialIndex].variants[currentVariantIndex];
        }
    }

    private void UpdateMainTextureUI()
    {
        Material currentMaterial = mainMaterials[currentMainMaterialIndex].mainMaterial;
        Texture2D albedoTexture = (Texture2D)currentMaterial.GetTexture("_MainTex");

        if (albedoTexture != null)
        {
            Sprite albedoSprite = Sprite.Create(albedoTexture, new Rect(0, 0, albedoTexture.width, albedoTexture.height), new Vector2(0.5f, 0.5f));
            mainTextures.sprite = albedoSprite;
        }
    }

    private void UpdateVariantTextureUI()
    {
        Material currentVariant = mainMaterials[currentMainMaterialIndex].variants[currentVariantIndex];
        Texture2D albedoTexture = (Texture2D)currentVariant.GetTexture("_MainTex");

        if (albedoTexture != null)
        {
            Sprite albedoSprite = Sprite.Create(albedoTexture, new Rect(0, 0, albedoTexture.width, albedoTexture.height), new Vector2(0.5f, 0.5f));
            variantTextures.sprite = albedoSprite;
        }
    }
}

[System.Serializable]
public class MainMaterial
{
    public Material mainMaterial;
    public List<Material> variants = new List<Material>();
}
