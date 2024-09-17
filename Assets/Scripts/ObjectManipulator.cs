using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectManipulator : MonoBehaviour
{
    public GameObject arObject;
    public TMP_Text text;
    [SerializeField] private Camera arCamera;

    [SerializeField] private float scaleFactor = 0.1f;

    private float touchDist;
    private float scaleTolerance = 20f;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touchOne = Input.GetTouch(0);

            if (touchOne.phase == TouchPhase.Began)
            {
                DetectCorner(touchOne);
            }

            ScaleAllObject(touchOne);
        }
    }

    void ScaleAllObject(Touch touchOne)
    {
        if (Input.touchCount == 2)
        {
            Touch touchTwo = Input.GetTouch(1);
            if (touchOne.phase == TouchPhase.Began || touchTwo.phase == TouchPhase.Began)
            {
                touchDist = Vector2.Distance(touchTwo.position, touchOne.position);
            }

            if (touchOne.phase == TouchPhase.Moved || touchTwo.phase == TouchPhase.Moved)
            {
                float currentTouchDis = Vector2.Distance(touchTwo.position, touchOne.position);

                float diffDis = currentTouchDis - touchDist;

                if (MathF.Abs(diffDis) > scaleTolerance)
                {
                    Vector3 newScale = arObject.transform.localScale + MathF.Sign(diffDis) * Vector3.one * scaleFactor;
                    arObject.transform.localScale = Vector3.Lerp(arObject.transform.localScale, newScale, 0.05f);
                }
            }
        }
    }

    void DetectCorner(Touch touch)
    {
        Ray ray = arCamera.ScreenPointToRay(touch.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform == arObject.transform)
            {
                Vector3[] corners = GetWorldCorners(arObject);
                Vector3 hitPoint = hit.point;

                // Encuentra la esquina más cercana al punto de impacto
                Vector3 closestCorner = corners[0];
                float closestDistance = Vector3.Distance(hitPoint, closestCorner);

                for (int i = 1; i < corners.Length; i++)
                {
                    float distance = Vector3.Distance(hitPoint, corners[i]);
                    if (distance < closestDistance)
                    {
                        closestCorner = corners[i];
                        closestDistance = distance;
                    }
                }

                text.text = "Esquina más cercana tocada: " + closestCorner;
                Debug.Log("Esquina más cercana tocada: " + closestCorner);
            }
        }
    }

    Vector3[] GetWorldCorners(GameObject obj)
    {
        MeshFilter meshFilter = obj.GetComponent<MeshFilter>();
        if (meshFilter == null) return null;

        Mesh mesh = meshFilter.mesh;
        Bounds bounds = mesh.bounds;

        // Las 4 esquinas en coordenadas locales
        Vector3[] localCorners = new Vector3[4];
        localCorners[0] = new Vector3(bounds.min.x, bounds.min.y, bounds.center.z); // Esquina inferior izquierda
        localCorners[1] = new Vector3(bounds.max.x, bounds.min.y, bounds.center.z); // Esquina inferior derecha
        localCorners[2] = new Vector3(bounds.min.x, bounds.max.y, bounds.center.z); // Esquina superior izquierda
        localCorners[3] = new Vector3(bounds.max.x, bounds.max.y, bounds.center.z); // Esquina superior derecha

        // Convertir las esquinas locales a espacio mundial
        Vector3[] worldCorners = new Vector3[4];
        for (int i = 0; i < 4; i++)
        {
            worldCorners[i] = obj.transform.TransformPoint(localCorners[i]);
        }

        return worldCorners;
    }

}
