using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSound : MonoBehaviour
{
    FireController fireController;
    private AudioSource audioSource;
    [SerializeField] private float addedVolumeMultiplier = 0.5f;
    [SerializeField] private AudioClip fireClip;
    float maxDistanceToPlayer = 20f;
    [SerializeField] float minimumFireSoundVolume = 0.3f;
    float fireSoundUpdateInterval = 0.2f;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>() as AudioSource;
        audioSource.loop = true;
        audioSource.clip = fireClip;
        audioSource.Play();
        fireController = GameObject.Find("MapManager").GetComponent<FireController>();
        InvokeRepeating(nameof(AdjustSound), 0f, fireSoundUpdateInterval);
    }

    void AdjustSound()
    {
        float distanceToPlayer = maxDistanceToPlayer;
        for (int y = 0; y < fireController.mapSizeY; y++)
        {
            for (int x = 0; x < fireController.mapSizeX; x++)
            {
                if(fireController.map[x, y].GetComponent<TileFire>().IsTileOnFire())
                {
                    float distanceToThisTile = Vector3.Distance(fireController.map[x, y].transform.position, transform.position);
                    if (distanceToThisTile < distanceToPlayer)
                        distanceToPlayer = distanceToThisTile;
                }    
            }
        }
        audioSource.volume = minimumFireSoundVolume + (maxDistanceToPlayer - distanceToPlayer) / maxDistanceToPlayer * addedVolumeMultiplier;
    }
}
