using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WallEditor : MonoBehaviour
{
    public TMP_InputField widthInputField;
    public TMP_InputField heightInputField;
    public TMP_Text debugText;
    public CreateQuad createQuadScript;

    public float minValue;
    public float maxValue;

    public GameObject panelEditWall;

    public void OnCreateWallButtonClick()
    {
        float width, height;

        if (float.TryParse(widthInputField.text, out width) && float.TryParse(heightInputField.text, out height))
        {
            if (IsValidDimension(width) && IsValidDimension(height))
            {
                // Establecer los valores en el script CreateQuad
                createQuadScript.width = width;
                createQuadScript.height = height;

                // Llamar al método CreateWall para crear el mesh
                createQuadScript.CreateWallQR();
                panelEditWall.SetActive(false);
            }
            else
            {
                debugText.text = $"Los valores deben estar entre {minValue} y {maxValue}";

                Debug.LogWarning($"Los valores deben estar entre {minValue} y {maxValue}");
            }
        }
        else
        {
            debugText.text = "Por favor, ingrese valores numéricos válidos.";

            Debug.LogWarning("Por favor, ingrese valores numéricos válidos.");
        }
    }

    private bool IsValidDimension(float value)
    {
        return value >= minValue && value <= maxValue;
    }

    public void ActiveDesactiveEditUI()
    {
        if(createQuadScript.meshFilterQR!=null)
        {
            panelEditWall.SetActive(!panelEditWall.activeSelf);
        }
        
    }
}
