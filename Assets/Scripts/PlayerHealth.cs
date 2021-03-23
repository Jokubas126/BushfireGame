﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private float health = 100f;
    [SerializeField]
    private float fireDamage = 10f;
    [SerializeField]
    private float burnReactivationTime = 0.5f;

    public bool isCheckingBurn = false;
    private Slider healthSlider;
    private Animator animator;


    void Start()
    {
        healthSlider = GameObject.Find("HealthBar").GetComponent<Slider>();
        healthSlider.maxValue = health;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCheckingBurn && IsTileOnFire())
        {
            StartCoroutine(burn());
        }
        death();
    }

    void OnGUI()
    {
        healthSlider.value = health;
    }

    private bool IsTileOnFire()
    {
        int layerMask = 1 << 9; // layer of map tile

        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 3, layerMask))
        {
            TileFire tileBelow = hit.collider.gameObject.GetComponentInParent<TileFire>();
            if (tileBelow != null)   //Make sure we are above tile
            {
                if(tileBelow.fireResistanceCurrent <= 0 && tileBelow.fireDuration > 0)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void death()
    {
        if(health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    IEnumerator burn()
    {
        isCheckingBurn = true;
        health -= fireDamage;
        animator.Play("UpperBody.Hurt");
        animator.Play("Hands.Hurt");
        yield return new WaitForSeconds(burnReactivationTime);
        isCheckingBurn = false;
        yield return null;
    }
}
