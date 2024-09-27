using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerTextures : MonoBehaviour
{
    public static ManagerTextures Instance;
    public Renderer selectedRenderer;
    public Material SelectedMaterial;

    private void Awake()
    {
        Instance = this;
    }

    public void ApplyVariantMaterial()
    {
        if (selectedRenderer != null && SelectedMaterial != null)
        {
            selectedRenderer.material = SelectedMaterial;
        }
    }
    public void SelectMaterial(Material material)
    {
        SelectedMaterial = material;
    }
}
