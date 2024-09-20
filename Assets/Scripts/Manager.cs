using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
public class Manager : MonoBehaviour
{
    public static Manager Instance;
    public GameObject arObject;
    public GameObject arObjectQR;
    public ARSession arSession;
    public ImageTracker imageTracker;

    public bool isReset;
    private void Awake()
    {
        Instance = this;
    }
    public void Exit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        isReset = true;

        foreach (var arObject in imageTracker.ARObjects)
        {
            if (arObject != null)
            {
                Destroy(arObject);
            }
        }
        imageTracker.ARObjects.Clear();
        if (arObject != null)
        {
            Destroy(arObject.gameObject);
        }
        
        arObject = null;
        // Reiniciar la sesión AR
        arSession.Reset();
        isReset = false;
    }
}
