using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

public class HoldObject : MonoBehaviour
{
    private GameObject pickedUpObject;
    private GameObject targetedObject;
    private Quaternion pickedObjectRotation;
    private float floorHeight = 0.5f;
    private Animator animator;
    public bool isUnderPlayerControl = true;
    public float interactionDistance = 10f;
    public float pickUpTime = 1f;
    private GameObject pickupHint;

    public bool IsHoldingObject
    {
        get => pickedUpObject != null;
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        pickupHint = GameObject.Find("Canvas").transform.Find("PickupHint").gameObject;
    }

    void Update()
    {
        targetedObject = HighlightPickupable();
        if (Input.GetKeyDown("f") && isUnderPlayerControl)
        {
            if (pickedUpObject == null)
            {
                if (targetedObject != null)
                {
                    StartCoroutine(PickUp());
                }
            }
            else StartCoroutine(Release());
        }
        CarryObject();
    }

    IEnumerator PickUp()
    {
        SetPlayerControl(false);
        PlayHoldingAnimation(true);
        targetedObject.transform.Find("koala").GetComponent<Outline>().enabled = false;
        yield return new WaitForSeconds(pickUpTime);
        if (targetedObject != null)
        {
            pickedUpObject = targetedObject;
            pickedObjectRotation = pickedUpObject.transform.rotation;
        }
        else
        {
            PlayHoldingAnimation(false);
        }
        SetPlayerControl(true);
    }

    private void CarryObject()
    {
        if (pickedUpObject != null)
        {
            pickedUpObject.transform.parent = transform;
            pickedUpObject.transform.position = transform.position + transform.forward;
        }
    }

    IEnumerator Release()
    {
        if (CanBeReleased())
        {
            SetPlayerControl(false);
            PlayHoldingAnimation(false);
            pickedUpObject.transform.position = GetPutPosition();
            pickedUpObject.transform.rotation = pickedObjectRotation;
            pickedUpObject.transform.parent = null;
            pickedUpObject.transform.Find("koala").GetComponent<Outline>().enabled = true;
            pickedUpObject = null;
            yield return new WaitForSeconds(pickUpTime);
            SetPlayerControl(true);
        }
    }

    private void PlayHoldingAnimation(bool isPickingUp)
    {
        if (isPickingUp)
        {
            animator.Play("UpperBody.KoalaUp");
            animator.Play("Hands.KoalaUp");
        }
        else
        {
            animator.Play("UpperBody.KoalaDown");
            animator.Play("Hands.KoalaDown");
        }
        animator.SetBool("isHolding", isPickingUp);
    }

    private Vector3 GetPutPosition()
    {
        Vector3 putPosition;
        putPosition.x = Mathf.Round(pickedUpObject.transform.position.x);
        putPosition.z = Mathf.Round(pickedUpObject.transform.position.z);
        putPosition.y = floorHeight + pickedUpObject.transform.lossyScale.y / 2;
        return putPosition;
    }

    private bool CanBeReleased()
    {
        int layerMask = 1 << 9; // layer of map tile

        if (Physics.Raycast(GetPutPosition(), Vector3.down, out RaycastHit hit, 3, layerMask))
        {
            switch (hit.collider.gameObject.tag)
            {
                case "Rock":
                case "Water":
                case "PickableObject":
                    return false;
                default:
                    return true;
            }
        }
        return false;
    }

    private GameObject HighlightPickupable()
    {
        int layerMask = 1 << 8; // layer of pickable object

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, interactionDistance, layerMask))
        {
            if (hit.collider.gameObject.CompareTag("PickableObject"))
            {
                pickupHint.SetActive(pickedUpObject == null && isUnderPlayerControl);
                hit.collider.gameObject.GetComponent<Highlightable>().Highlight();
                return hit.collider.gameObject;
            }
        }
        pickupHint.SetActive(false);
        return null;
    }

    private void SetPlayerControl(bool isPlayerControl)
    {
        isUnderPlayerControl = isPlayerControl;
        GetComponent<PlayerMovement>().isUnderPlayerControl = isPlayerControl;
        transform.Find("WaterHose").GetComponent<FireExtinguisher>().isUnderPlayerControl = isPlayerControl;
    }
}
