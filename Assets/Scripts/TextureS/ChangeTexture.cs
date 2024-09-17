using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTexture : MonoBehaviour
{
    public ObjectManipulator objectManipulator;
    public Material quadMaterial;
    public Texture texture1;     
    public Texture texture2;       

    public List<MainTextures> mainTextures = new List<MainTextures>();

    private bool isUsingTexture1 = true;
    private bool once;

    private void Update()
    {
        if (objectManipulator.arObject != null && !once)
        {
            quadMaterial = objectManipulator.arObject.GetComponent<Renderer>().material;
            once = true;
        }
    }
    private void SetTexture1()
    {
        if (quadMaterial != null && texture1 != null)
        {
            quadMaterial.mainTexture = texture1;
            isUsingTexture1 = true;
        }
    }

    private void SetTexture2()
    {
        if (quadMaterial != null && texture2 != null)
        {
            quadMaterial.mainTexture = texture2;
            isUsingTexture1 = false;
        }
    }

    public void ToggleTexture()
    {
        if (!objectManipulator.arObject)
        {
            if (isUsingTexture1)
            {
                SetTexture2();
            }
            else
            {
                SetTexture1();
            }
        }
        
    }


}

[System.Serializable]
public class MainTextures
{
    public Texture principalTexture;
    public List<Texture> variantTextures = new List<Texture>();
}
