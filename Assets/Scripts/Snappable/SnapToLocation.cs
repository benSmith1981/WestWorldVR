using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToLocation : MonoBehaviour
{
	private bool grabbed = false;
	private bool insideSnapZone = false;

	public bool snapped;
	public Transform SnapPosition;

	public GameObject RocketPart;
	public GameObject SnapRotationReference;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
    	Debug.Log("SnapToLocation *****  OnTriggerEnter");

		if(other.gameObject.tag == RocketPart.tag) {
			insideSnapZone = true;

    	}
    }

    private void OnTriggerExit(Collider other) {
    	Debug.Log("SnapToLocation *****  OnTriggerExit");

    	if(other.gameObject.tag == RocketPart.tag) {
			insideSnapZone = false;

    	}
    }

    void snapObject() {
    	if(grabbed == false && insideSnapZone == true) {
	    	Debug.Log("OnTriggerEnter");
	    	RocketPart.gameObject.transform.position = transform.position;
			RocketPart.gameObject.transform.rotation = SnapRotationReference.transform.rotation;
			snapped = true;   	
    	} else {
    		snapped = false; 
    	}

    }


    // Update is called once per frame
    void Update()
    {
        grabbed = RocketPart.GetComponent<OVRGrabbable>().isGrabbed; 
        snapObject();
    }
}
