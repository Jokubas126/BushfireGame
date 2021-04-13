using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileFire : MonoBehaviour
{
    public float fireResistanceMax;
    public float fireResistanceCurrent = 0; //If this is 0, tile is considered on fire
    public float fireDuration; //If we want fire to be able to die out on its own
    private ParticleSystem fireParticles;
    private Color originalColor;
    public bool isFireStartTile = false;
    public bool isTileBurnable;
    public bool isColorStatic;

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
            if (IsTileOnFire())
            {
                if (!fireParticles.isEmitting && isTileBurnable)
                {
                    fireParticles.Play();
                }
            }
            else
            {
                if (fireParticles.isPlaying && fireResistanceCurrent > 0 || fireParticles.isPlaying && fireDuration <= 0)
                {
                    fireParticles.Stop();
                }
            }
        }
        if (fireResistanceCurrent > 0 && !isColorStatic)
        {
            Color.RGBToHSV(originalColor, out _, out float S, out float V);
            float H = 0.1f + fireResistanceCurrent / fireResistanceMax * 0.13f;
            GetComponentInChildren<Renderer>().material.color = Color.HSVToRGB(H, S, V);
        }
    }

    public void IncreaseTileResistance(float resistanceAddition)
    {
        fireResistanceCurrent = Mathf.Min(fireResistanceCurrent + resistanceAddition, fireResistanceMax);
    }

    public bool IsTileOnFire()
    {
        return fireResistanceCurrent <= 0 && fireDuration > 0 && isTileBurnable;
    }
}
