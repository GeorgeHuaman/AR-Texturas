using UnityEngine;

public class TextureScaler : MonoBehaviour
{
    public Material quadMaterial;
    private Vector3 previousScale;

    void Start()
    {
        GetMaterial();
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
        if (quadMaterial!= null)
        {
            Vector2 textureTiling = new Vector2(transform.localScale.x, transform.localScale.y);
            quadMaterial.mainTextureScale = textureTiling;
        }
        
    }
    void GetMaterial()
    {
        Renderer renderer = GetComponent<Renderer>();

        if (renderer != null)
        {
            quadMaterial = renderer.material;
            previousScale = transform.localScale;

            UpdateTextureTiling();
        }
        previousScale = transform.localScale;

        UpdateTextureTiling();
    }
}
