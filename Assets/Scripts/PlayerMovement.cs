using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    public float playerSpeed = 5.0f;
    public float rotateSpeed = 250.0f;

    private static readonly float grassMovementCoef = 1f;
    private static readonly float treeMovementCoef = 0.35f;
    private static readonly float waterMovementCoef = 0.5f;
    private static readonly float bushMovementCoef = 0.7f;

    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 move = Vector3.ClampMagnitude(moveInput, 1f);
        controller.Move(move * Time.fixedDeltaTime * playerSpeed * GetMovementCoef());

        if (move != Vector3.zero)
        {
            var r = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, r, rotateSpeed * Time.deltaTime);
        }
    }

    private float GetMovementCoef()
    {
        int layerMask = 1 << 9; // layer of map tile

        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 3, layerMask))
        {
            switch (hit.collider.gameObject.tag)
            {
                case "Grass":
                    return grassMovementCoef;
                case "Bush":
                    return bushMovementCoef;
                case "Tree":
                    return treeMovementCoef;
                case "Water":
                    return waterMovementCoef;
            }
        }
        return 1f;
    }

}
