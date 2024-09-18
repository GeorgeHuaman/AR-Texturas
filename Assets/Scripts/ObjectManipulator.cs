using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectManipulator : MonoBehaviour
{
    public GameObject arObject;

    [SerializeField] private Camera arCamera;
    public TMP_Text text;
    [SerializeField] private float scaleFactor = 0.1f;

    private float touchDist;

    private float scaleTolerance = 20f;
    private BoxCollider touchedSide = null;
    private Vector2 initialTouchPosition;
    private Vector3 initialScale;
    private Vector3 initialPosition;
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touchOne = Input.GetTouch(0);

            ScaleAllObject(touchOne);

            if (touchOne.phase == TouchPhase.Began)
            {
                //DetectSide(touchOne);
                if (touchedSide != null)
                {
                    initialTouchPosition = touchOne.position;
                    initialScale = arObject.transform.localScale;
                    initialPosition = arObject.transform.position;
                }
            }
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
    //void ScaleQuadSide(Touch touch)
    //{
    //    if (Input.touchCount == 1)
    //    {
    //        if (touchedSide != null)
    //        {
    //            // Obtener la diferencia entre la posición del toque actual y la inicial
    //            Vector2 deltaPosition = touch.position - initialTouchPosition;

    //            // Obtener las escalas y posiciones actuales
    //            Vector3 currentScale = arObject.transform.localScale;
    //            Vector3 currentPosition = arObject.transform.position;

    //            // Ajustar la escala en Y y la posición en Z
    //            float scaleChangeY;
    //            float positionChangeZ;

    //            if (touchedSide.name == "Top")
    //            {
    //                // Si se mueve hacia arriba, aumentar la escala en Y
    //                scaleChangeY = deltaPosition.y * scaleFactor;

    //                // Ajustar la nueva escala Y
    //                currentScale.y = Mathf.Clamp(initialScale.y + scaleChangeY, 0.1f, 2f);

    //                // Ajustar la posición en Z para mover el objeto hacia abajo mientras se escala
    //                positionChangeZ = (currentScale.y - initialScale.y) / 2;
    //                currentPosition.z = initialPosition.z - positionChangeZ;
    //            }
    //            else if (touchedSide.name == "Bottom")
    //            {
    //                // Si se mueve hacia abajo, reducir la escala en Y
    //                scaleChangeY = deltaPosition.y * scaleFactor;

    //                // Ajustar la nueva escala Y
    //                currentScale.y = Mathf.Clamp(initialScale.y - scaleChangeY, 0.1f, 2f);

    //                // Ajustar la posición en Z para mover el objeto hacia arriba mientras se escala
    //                positionChangeZ = (initialScale.y - currentScale.y) / 2;
    //                currentPosition.z = initialPosition.z + positionChangeZ;
    //            }

    //            // Aplicar los cambios de escala y posición
    //            arObject.transform.localScale = Vector3.Lerp(arObject.transform.localScale, currentScale, 0.1f);
    //            arObject.transform.position = Vector3.Lerp(arObject.transform.localPosition, currentPosition, 0.1f);
    //        }
    //    }
    //}

    void DetectSide(Touch touch)
    {
        Ray ray = arCamera.ScreenPointToRay(touch.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null)
            {
                touchedSide = hit.collider as BoxCollider;
                text.text = "Lado del Quad tocado: " + touchedSide.name;
                Debug.Log("Lado del Quad tocado: " + touchedSide.name);
            }
        }
    }
}
