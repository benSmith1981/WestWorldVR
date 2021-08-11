using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnappableObject : MonoBehaviour
{
    public GameObject snapObject;
    public bool isSnapped = false;

	private bool grabbed = false;
    private bool objectSnapped = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        grabbed = GetComponent<OVRGrabbable>().isGrabbed; 
        objectSnapped = snapObject.GetComponent<SnapToLocation>().snapped;
        if(objectSnapped == true) {
            GetComponent<Rigidbody>().isKinematic = true; 
            transform.SetParent(snapObject.transform);
            isSnapped = true;
        }
        if(objectSnapped == false && grabbed == false) {
            GetComponent<Rigidbody>().isKinematic = false; 
        }
    }
}