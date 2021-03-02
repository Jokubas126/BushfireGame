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
        if (Input.GetKey("e"))
        {
            int layerMask = 1 << 9;

            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance, layerMask))
            {

            }
        }
    }
}
