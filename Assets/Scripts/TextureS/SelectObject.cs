using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectObject : MonoBehaviour
{
    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android ||
            Application.platform == RuntimePlatform.IPhonePlayer)
        {
            OnMobile();
        }
        else
        {
            OnPc();
        }
    }
    public void OnPc()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("ARObject"))
                {
                    ManagerTextures.Instance.selectedRenderer = hit.transform.GetComponent<Renderer>();
                    ManagerTextures.Instance.ApplyVariantMaterial();
                }
            }
        }
    }
    public void OnMobile()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (EventSystem.current.IsPointerOverGameObject())
                return;

            if (touch.phase == TouchPhase.Began)

                if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                    return;

            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("ARObject"))
                {
                    ManagerTextures.Instance.selectedRenderer = hit.transform.GetComponent<Renderer>();
                    ManagerTextures.Instance.ApplyVariantMaterial();
                }
            }
        }
    }
}
