using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldObject : MonoBehaviour
{
    private GameObject pickedUpObject;
    private GameObject targetedObject;
    private Quaternion pickedObjectRotation;
    private float floorHeight = 0.5f;

    public float interactionDistance = 10f;

    public bool IsHoldingObject
    {
        get => pickedUpObject != null;
    }

    void Update()
    {
        targetedObject = HighlightPickupable();
        if (Input.GetKeyDown("f"))
        {
            if (pickedUpObject == null)
            {
                if (targetedObject != null)
                {
                    PickUp();
                }
            }
            else Release();
        }
        CarryObject();
    }

    public GameObject GetPickedUpObject()
    {
        return pickedUpObject;
    }

    private void PickUp()
    { 
        pickedUpObject = targetedObject;
        pickedObjectRotation = pickedUpObject.transform.rotation;
    }

    private void CarryObject()
    {
        if (pickedUpObject != null)
        {
            pickedUpObject.transform.parent = transform;
            pickedUpObject.transform.position = transform.position + transform.forward;
        }
    }

    private void Release()
    {
        pickedUpObject.transform.position = GetPutPosition();
        pickedUpObject.transform.rotation = pickedObjectRotation;
        pickedUpObject.transform.parent = null;
        pickedUpObject = null;
    }

    private Vector3 GetPutPosition()
    {
        Vector3 putPosition;
        putPosition.x = pickedUpObject.transform.position.x;
        putPosition.z = pickedUpObject.transform.position.z;
        putPosition.y = floorHeight + pickedUpObject.transform.lossyScale.y / 2;
        return putPosition;
    }

    private GameObject HighlightPickupable()
    {
        int layerMask = 1 << 8; // layer of pickable object

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, interactionDistance, layerMask))
        {
            if (hit.collider.gameObject.CompareTag("PickableObject"))
            {
                hit.collider.gameObject.GetComponent<Highlightable>().Highlight();
                return hit.collider.gameObject;
            }
        }
        return null;
    }
}
