using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;

public class PlaneManager : MonoBehaviour
{
    [SerializeField] private ARPlaneManager arpPlaneManager;
    public GameObject model3dPrefab;
    public ImageTracker imageTracker;

    public List<ARPlane> planes = new List<ARPlane>();
    private GameObject model3DPlaced;

    [SerializeField] private CreateQuad createQuadScript; // Referencia al script CreateQuad
    [SerializeField] private bool detectWallActive;

    public TMP_Text text;
    
    private void OnEnable()
    {
        arpPlaneManager.planesChanged += PlanesFound;
    }
    private void OnDisable()
    {
        arpPlaneManager.planesChanged -= PlanesFound;
    }
    private void Update()
    {
        if (detectWallActive)
        {
            text.text = "Parar";
        }
        else
        {
            text.text = "Detectar Pared";
        }
    }
    private void PlanesFound(ARPlanesChangedEventArgs planeData)
    {
        if (detectWallActive)
        {
            if (planeData.added != null && planeData.added.Count > 0)
            {
                planes.AddRange(planeData.added);
            }

            foreach (ARPlane plane in planes)
            {
                if (plane.extents.x * plane.extents.y >= 0.4f)
                {
                    // Obtener las dimensiones del plano detectado
                    float planeWidth = plane.size.y * 100;
                    float planeHeight = plane.size.x * 100;

                    createQuadScript.width = planeWidth;
                    createQuadScript.height = planeHeight;


                    if (model3DPlaced == null)
                    {
                        model3DPlaced = Instantiate(model3dPrefab);
                        Manager.Instance.arObject = model3DPlaced;
                    }
                       
                        
                    model3DPlaced.transform.position = new Vector3(plane.center.x, plane.center.y, plane.center.z);
                    model3DPlaced.transform.forward = -plane.normal;
                    createQuadScript.meshFilter=model3DPlaced.GetComponent<MeshFilter>(); 
                    createQuadScript.CreateWall();


                }
            }
        }
        
    }

    public void StopPlaneDetection()
    {
        arpPlaneManager.requestedDetectionMode = UnityEngine.XR.ARSubsystems.PlaneDetectionMode.None;
        foreach (var plane in planes)
        {
            if (plane !=null)
            {
                plane.gameObject.SetActive(false);
            }
           
        }
        planes.Clear();
    }
    public void StartPlaneDetection()
    {
        arpPlaneManager.requestedDetectionMode = UnityEngine.XR.ARSubsystems.PlaneDetectionMode.Vertical;

    }

    public void ActiveDesactiateDetection()
    {
        detectWallActive= !detectWallActive;
        if (detectWallActive)
        {
            StartPlaneDetection();
        }
        else
        {
            StopPlaneDetection();
        }
    }

}
