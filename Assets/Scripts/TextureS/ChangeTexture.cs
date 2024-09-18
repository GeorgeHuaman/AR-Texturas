using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTexture : MonoBehaviour
{
    public ObjectManipulator objectManipulator;
    public Renderer quadRenderer; // Cambiado de Material a Renderer para acceder a los materiales
    public Renderer quadRendererQr;
    public List<MainMaterial> mainMaterials; // Lista de MainMaterial en lugar de texturas

    [SerializeField] private int currentMainMaterialIndex = 0; // Índice del material principal actual
    [SerializeField] private int currentVariantIndex = 0; // Índice de la variante actual

    private bool once;
    private bool onceQR;

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
            currentMainMaterialIndex = (currentMainMaterialIndex + 1) % mainMaterials.Count;
            currentVariantIndex = 0;
            ApplyMainMaterial();
        }
    }

    public void ChangeVariantMaterial()
    {
        if (mainMaterials.Count > 0 && mainMaterials[currentMainMaterialIndex].variants.Count > 0)
        {
            currentVariantIndex = (currentVariantIndex + 1) % mainMaterials[currentMainMaterialIndex].variants.Count;
            ApplyVariantMaterial();
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

}

[System.Serializable]
public class MainMaterial
{
    public Material mainMaterial;
    public List<Material> variants = new List<Material>();
}
