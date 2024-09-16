using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
public class ImageTracker : MonoBehaviour
{
    private ARTrackedImageManager trackedImages;
    public GameObject[] ArPrefabs;

    public List<GameObject> ARObjects = new List<GameObject>();
    public ARSession arSession;
    private bool isReset;
    void Awake()
    {
        trackedImages = GetComponent<ARTrackedImageManager>();
    }

    void OnEnable()
    {
        trackedImages.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        trackedImages.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    // Event Handler
    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        //Create object based on image tracked
        foreach (var trackedImage in eventArgs.added)
        {
            foreach (var arPrefab in ArPrefabs)
            {
                if (trackedImage.referenceImage.name == arPrefab.name && ARObjects.Count == 0)
                {
                    //para android
                    //var newPrefab = Instantiate(arPrefab, trackedImage.transform);

                    //para IOS
                    var newPrefab = Instantiate(arPrefab, trackedImage.transform.position, Quaternion.identity);

                    ARObjects.Add(newPrefab);
                }
            }
        }

        if(!isReset)
        {
            //Update tracking position
            foreach (var trackedImage in eventArgs.updated)
            {
                foreach (var gameObject in ARObjects)
                {
                    if (gameObject.name == trackedImage.name)
                    {
                        gameObject.SetActive(trackedImage.trackingState == TrackingState.Tracking);
                    }
                }
            }
        }
        

        
    }

    public void Refresh()
    {
        int i = 0;

        foreach (var trackedImage in trackedImages.trackables)
        {
            if (trackedImage.trackingState == TrackingState.Limited)
            {
                ARObjects[i].SetActive(false);
            }

            if (trackedImage.trackingState == TrackingState.Tracking)
            {
                ARObjects[i].SetActive(true);
            }
            i++;
        }
    }

    public void Refresh2()
    {

        isReset = true;
        // Limpiar todos los objetos AR instanciados para evitar duplicados
        foreach (var arObject in ARObjects)
        {
            if (arObject != null)
            {
                Destroy(arObject/*.transform.parent.gameObject*/);


            }
        }
        ARObjects.Clear();
        // Reiniciar la sesión AR
        arSession.Reset();
        isReset = false;
    }
}