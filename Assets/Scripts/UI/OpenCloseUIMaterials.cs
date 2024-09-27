using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseUIMaterials : MonoBehaviour
{
    public GameObject scrollview;

    public void ActiveAndDesactive()
    {
        scrollview.SetActive(!scrollview.activeSelf);
    }
}
