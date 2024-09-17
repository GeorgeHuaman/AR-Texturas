using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManipulator : MonoBehaviour
{
    public GameObject arObject;

    [SerializeField] private Camera arCamera;

    [SerializeField] private float scaleFactor = 0.1f;

    private float touchDist;

    private float scaleTolerance = 20f;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touchOne = Input.GetTouch(0);

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

}
