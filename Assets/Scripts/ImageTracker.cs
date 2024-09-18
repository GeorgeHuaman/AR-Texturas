using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
public class ImageTracker : MonoBehaviour
{
    public PlaneManager planeManager;
    private ARTrackedImageManager trackedImages;
    public GameObject[] ArPrefabs;

    public List<GameObject> ARObjects = new List<GameObject>();
    public ARSession arSession;
    public ObjectManipulator objectManipulator;
    public CreateQuad CreateQuad;
    public GameObject wallEditorUI;
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

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        //Create object based on image tracked
        foreach (var trackedImage in eventArgs.added)
        {
            foreach (var arPrefab in ArPrefabs)
            {
                if (trackedImage.referenceImage.name == arPrefab.name && ARObjects.Count == 0)
                {
                    var newPrefab = Instantiate(arPrefab, trackedImage.transform);

                    ARObjects.Add(newPrefab);
                    Manager.Instance.arObjectQR = newPrefab;
                    wallEditorUI.SetActive(true);
                }
            }
        }
        if (!Manager.Instance.isReset)
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

}