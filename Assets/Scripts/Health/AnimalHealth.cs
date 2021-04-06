using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalHealth : MonoBehaviour
{
    [SerializeField]
    private float health = 100f;
    [SerializeField]
    private float fireDamage = 10f;
    [SerializeField]
    private float burnReactivationTime = 0.5f;

    private HealthManager healthManager;
    [SerializeField]
    private AudioClip[] koalaHurtSound = new AudioClip[0];
    private AudioSource audioSourceHurt;

    void Start()
    {
        healthManager = new HealthManager(health, fireDamage, burnReactivationTime);
        audioSourceHurt = gameObject.AddComponent<AudioSource>() as AudioSource;
    }


    void Update()
    {

        if (healthManager.IsAllowedToBurn(transform.position))
        {
            StartCoroutine(healthManager.Burn());
            PlayHurtSound();
            
        }
        if (healthManager.IsDead)
        {
            Debug.Log("Koala is dead");
            GameObject.Find("Canvas").GetComponent<Score>().AnimalDied();
            Destroy(gameObject);
        }
    }
    void PlayHurtSound()
    {
        if (audioSourceHurt != null && !audioSourceHurt.isPlaying)
        {
            int n = Random.Range(1, koalaHurtSound.Length);
            audioSourceHurt.clip = koalaHurtSound[n];
            audioSourceHurt.Play();
            koalaHurtSound[n] = koalaHurtSound[0];
            koalaHurtSound[0] = audioSourceHurt.clip;
        }
    }
    

}

