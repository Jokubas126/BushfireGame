using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldObject : MonoBehaviour
{
    private GameObject pickedUpObject;
    public float interactionDistance = 10f;

    void Update()
    {
        if (Input.GetKeyDown("e") && pickedUpObject == null)
        {
            PickUp();
        }

        if (pickedUpObject != null)
        {
            CarryObject();

            if (Input.GetKey("r"))
            {
                Release();
            }
        }
    }

    private void PickUp()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, interactionDistance, 1 << 8))
        {
            if (hit.collider.gameObject.CompareTag("PickableObject"))
            {
                pickedUpObject = hit.collider.gameObject;
            }
        }
    }

    private void CarryObject()
    {
        pickedUpObject.transform.parent = transform;
        pickedUpObject.transform.position = transform.position + transform.forward;
    }

    private void Release()
    {
        pickedUpObject.transform.position = GetPutPosition();
        pickedUpObject.transform.parent = null;
        pickedUpObject = null;
    }

    private Vector3 GetPutPosition()
    {
        Vector3 putPosition = GameObject.FindGameObjectWithTag("Ground").transform.position;
        putPosition.x = pickedUpObject.transform.position.x;
        putPosition.z = pickedUpObject.transform.position.z;
        putPosition.y += pickedUpObject.transform.lossyScale.y / 2;
        return putPosition;
    }
}
