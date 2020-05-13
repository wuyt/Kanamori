using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class bbb : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    void Update()
    {

            // if (Input.GetMouseButtonDown(0)
            // && !EventSystem.current.IsPointerOverGameObject())
            // {
            //     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //     TouchedObject(ray);
            // }

            // if (Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Began
            // && !EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId))
            // {
            //     Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            //     TouchedObject(ray);
            // }
        
    }

    private void TouchedObject(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log(hit.transform.name);
        }
    }
}
