using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnappableObject : MonoBehaviour
{
    // public Transform target;
    // public Vector3 offset;

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


    }

     private void OnTriggerEnter(Collider other)
    {
        Debug.Log("SnapToLocation *****  OnTriggerEnter");

        objectSnapped = snapObject.GetComponent<SnapToLocation>().snapped;
        if(objectSnapped == true) {
            // GetComponent<Rigidbody>().isKinematic = false; 
            // GetComponent<Rigidbody>().useGravity = false;
            isSnapped = true;
            transform.SetParent(snapObject.transform);
            //disablePhysics();

        }
    }

    private void OnTriggerExit(Collider other) {
        Debug.Log("SnapToLocation *****  OnTriggerExit");

        if(objectSnapped == false && grabbed == true) {
            // GetComponent<Rigidbody>().isKinematic = false; 
            //GetComponent<Rigidbody>().useGravity = true; 
            isSnapped = false;
            //enablePhysics();
        }
    }


    void enablePhysics() {
        Rigidbody rb = transform.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.None;
    }

    void disablePhysics() {
        Rigidbody rb = transform.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }



}