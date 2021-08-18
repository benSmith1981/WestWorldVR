using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCasting : MonoBehaviour
{
    [SerializeField] Transform gunAim;
    [SerializeField] LineRenderer lineRend;
    Ray ray;
    RaycastHit hit;
    Camera cam;
    void Start()
    {
        cam = Camera.main;
    }


    void Update()
    {
        ray = cam.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray,out hit, 100))
        {
            lineRend.enabled = true;
            lineRend.SetPosition(0, gunAim.transform.position);
            lineRend.SetPosition(1, hit.point);

            if (Input.GetMouseButtonDown(0))
            {
                Destroy(hit.transform.gameObject);
            }
        }
        else
        {
            lineRend.enabled = false;
        }
    }
}
