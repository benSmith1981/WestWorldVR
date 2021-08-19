using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Item: MonoBehaviour
{
	public enum ItemType
	{
	SWORD,
	PISTOL
	}
	public ItemType itemType;
	public bool isSnapped = true;
	public InventorySlot hoveredInventorySlot;

	private bool grabbed = false;



    // Start is called before the first frame update
    void Start()
    {
        // m_Text = GameObject_m_Text.GetComponent<TextMeshProUGUI>();// 
  //       m_Text = gameObject.AddComponent<TextMeshPro>();
        // m_Text.text = "test";
  //       m_Text.autoSizeTextContainer = true;
		// m_Text.fontSize = 0.2f;

		isSnapped = true;
	}

	public void Update() {
		grabbed = GetComponent<OVRGrabbable>().isGrabbed; 
		// m_Text.text = grabbed ? "Grabbed" : "Not Grabbed";
		if(grabbed && isSnapped){
			OnPickup();
			// m_Text.text = "6";
			 // m_Text.text = grabbed ? "grabbed" : "Not grabbed";

		} else if (isSnapped && !grabbed){
			OnDrop();
		}

   	}

	public void OnTriggerEnter(Collider other) 
	{
		if (isSnapped)
		{
			return;

		}
		// m_Text.text = other.tag+" ... grabbed: " + grabbed + " isSnapped:" + isSnapped;
		if(other.tag == "InventorySlot"){
			if(!grabbed && !isSnapped){
			    // m_Text.text = other.tag+" ... grabbed: " + grabbed + " isSnapped:" + isSnapped;

				OnDrop();
			}
		}
  
		ShowInventory(other);

	}

	public void OnTriggerExit(Collider other)
	{
		if (isSnapped)
		{
			return;

		}
		HideInventory(other);
	}

	void ShowInventory(Collider other) {
		InventorySlot inventorySlot = other.transform.GetComponent<InventorySlot>();
		if (inventorySlot)
		{
			hoveredInventorySlot = inventorySlot;
			hoveredInventorySlot.TogglePlaceholderMesh(itemType, true);
		}
	}

	void HideInventory(Collider other){
		InventorySlot inventorySlot = other.transform.GetComponent<InventorySlot>();
		if (hoveredInventorySlot == inventorySlot)
		{
			hoveredInventorySlot.TogglePlaceholderMesh(itemType, false);
			hoveredInventorySlot = null;
		}
	}

    public void OnPickup()
    {
    	//if(!isSnapped && grabbed) return;
        if (isSnapped)
        {
            DetachFromInventorySlot();
        }
        else
        {
            hoveredInventorySlot = null;
        }
    }

    public void OnDrop()
    {
    	//if(isSnapped)return;
        if (hoveredInventorySlot != null && !hoveredInventorySlot.isOccupied)
        {
            AttachToInventorySlot();
        }
    }

    public void DetachFromInventorySlot()
    {
        if (hoveredInventorySlot != null)
        {
            hoveredInventorySlot.isOccupied = false;
            hoveredInventorySlot = null;
        }
        isSnapped = false;
        transform.SetParent(null);
        EnablePhysics();
    }

    public void AttachToInventorySlot()
    {
        isSnapped = true;
        hoveredInventorySlot.isOccupied = true;
        int indexOfPosition = hoveredInventorySlot.supportedItemTypes.IndexOf(itemType);
        Transform slotTransform = hoveredInventorySlot.itemPositions[indexOfPosition];
        transform.parent = slotTransform;
        transform.position = slotTransform.position;
        transform.rotation = slotTransform.rotation;
        DisablePhysics();
    }

	void EnablePhysics() {
        Rigidbody rb = transform.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.None;
    }

    void DisablePhysics() {
        Rigidbody rb = transform.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }
}
