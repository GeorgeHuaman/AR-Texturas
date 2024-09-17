using UnityEngine;

public class TextureScaler : MonoBehaviour
{
    public Material quadMaterial;
    private Vector3 previousScale;

    void Start()
    {
        quadMaterial = GetComponent<Material>();
        previousScale = transform.localScale;

        UpdateTextureTiling();
    }

    void Update()
    {
        if (transform.localScale != previousScale)
        {
            UpdateTextureTiling();
            previousScale = transform.localScale;
        }
    }

    void UpdateTextureTiling()
    {
        Vector2 textureTiling = new Vector2(transform.localScale.x, transform.localScale.y);
        quadMaterial.mainTextureScale = textureTiling;
    }
}
