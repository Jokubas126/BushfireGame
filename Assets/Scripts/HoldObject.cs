using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldObject : MonoBehaviour
{
    private GameObject pickedUpObject;
    public float interactionDistance = 10f;

    // Update is called once per frame
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

    void PickUp()
    {
        int layerMask = 1 << 8;

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, interactionDistance, layerMask))
        {
            if (hit.collider.gameObject.CompareTag("PickableObject"))
            {
                pickedUpObject = hit.collider.gameObject;
                
            }
        }
    }

    void CarryObject()
    {
        pickedUpObject.transform.parent = transform;
        pickedUpObject.transform.position = transform.position + transform.forward;
    }

    void Release()
    {
        pickedUpObject.transform.parent = null;
        pickedUpObject = null;
    }
}
