using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item: MonoBehaviour
{
	public enum ItemType
	{
	SWORD,
	PISTOL
	}
	public ItemType itemType;
	public bool isSnapped;
	public InventorySlot hoveredInventorySlot;
	public void Start()
	{
		isSnapped = false;
	}


	public void OnTriggerEnter(Collider other) {
		if (isSnapped)
		{
			return;
		}

		InventorySlot inventorySlot = other.transform.GetComponent<InventorySlot>();
		if (inventorySlot)
		{
			hoveredInventorySlot = inventorySlot;
			hoveredInventorySlot.TogglePlaceholderMesh(itemType, true);
		}
	}

	public void OnTriggerExit(Collider other)
	{
		if (isSnapped)
		{
			return;
		}

		InventorySlot inventorySlot = other.transform.GetComponent<InventorySlot>();
		if (hoveredInventorySlot == inventorySlot)
		{
			hoveredInventorySlot.TogglePlaceholderMesh(itemType, false);
			hoveredInventorySlot = null;
		}
	}

    public void OnPickup()
    {
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
