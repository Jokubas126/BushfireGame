﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileFire : MonoBehaviour
{
    public float fireResistanceMax;
    public float fireResistanceCurrent; //If this is 0, tile is considered on fire
    public int fireDuration; //If we want fire to be able to die out on its own
    private ParticleSystem fireParticles;
    private Color originalColor;

    void Start()
    {
        originalColor = GetComponentInChildren<Renderer>().material.color;
        fireParticles = GetComponentInChildren<ParticleSystem>();
        if (fireParticles != null)
            fireParticles.Stop();
        //fireResistanceMax = Random.Range(5, 100); //Moved to tile initialization
        //fireResistanceCurrent = fireResistanceMax;
    }

    void Update()
    {
        if (fireParticles != null)
        {
            if (fireResistanceCurrent == 0)
            {
                if (!fireParticles.isEmitting)
                {
                    fireParticles.Play();
                }
            }
            else
            {
                float H, S, V;
                Color.RGBToHSV(originalColor, out H, out S, out V);
                H = 0.1f + fireResistanceCurrent / fireResistanceMax * 0.13f;
                GetComponentInChildren<Renderer>().material.color = Color.HSVToRGB(H, S, V);
            }
        }
    }
}