﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    public float playerSpeed = 5.0f;
    private Animator animator;
    public float rotateSpeed = 250.0f;
    public int noMovementRotSteps = 16;
    public bool isUnderPlayerControl = true;
    public float koalaCarrySpeedMultiplier = 0.8f;

    private static readonly float grassMovementCoef = 1f;
    private static readonly float treeMovementCoef = 0.35f;
    private static readonly float waterMovementCoef = 0.5f;
    private static readonly float bushMovementCoef = 0.7f;

    [SerializeField] private AudioClip[] footstepSounds;
    private AudioSource audioSource;

    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
        animator = GetComponent<Animator>();
        audioSource = gameObject.AddComponent<AudioSource>() as AudioSource;
    }

    void FixedUpdate()
    {
        if (isUnderPlayerControl)
        {
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            move = Vector3.ClampMagnitude(move, 1f);
            if(GetComponent<HoldObject>().IsHoldingObject)
                controller.Move(move * Time.fixedDeltaTime * playerSpeed * GetMovementCoef() * koalaCarrySpeedMultiplier);
            else
                controller.Move(move * Time.fixedDeltaTime * playerSpeed * GetMovementCoef());
            if (move != Vector3.zero)
            {
                animator.SetBool("isWalking", true);
                var r = Quaternion.LookRotation(move);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, r, rotateSpeed * Time.fixedDeltaTime);
                PlayFootSteps();

            }
            else
            {
                animator.SetBool("isWalking", false);
                Vector3 newRotation = transform.rotation.ToEulerAngles();
                newRotation.y = Mathf.Round(newRotation.y * 180 / Mathf.PI / (360f / noMovementRotSteps)) * (360f / noMovementRotSteps);
                Quaternion rotation = Quaternion.Euler(newRotation);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotateSpeed * Time.fixedDeltaTime);
            }
        }
    }
    private void PlayFootSteps()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            int n = Random.Range(1, footstepSounds.Length);
            audioSource.clip = footstepSounds[n];
            audioSource.Play();
            footstepSounds[n] = footstepSounds[0];
            footstepSounds[0] = audioSource.clip;
        }
        audioSource.volume = 0.1F;
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
