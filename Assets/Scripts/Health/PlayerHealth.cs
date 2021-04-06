using System.Collections;
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
    [SerializeField]
    private AudioClip[] hurtSound = new AudioClip[0];
    private AudioSource audioSource;

    private Slider healthSlider;

    private HealthManager healthManager;

    Animator animator;

    void Start()
    {
        healthSlider = GameObject.Find("HealthBar").GetComponent<Slider>();
        healthSlider.maxValue = health;
        animator = GetComponent<Animator>();

        healthManager = new HealthManager(health, fireDamage, burnReactivationTime);
        audioSource = gameObject.AddComponent<AudioSource>() as AudioSource;
    }

    void Update()
    {
        if (healthManager.IsAllowedToBurn(transform.position))
        {
            StartCoroutine(healthManager.Burn());
            StartCoroutine(BurnAnimate());
        }
        if (healthManager.IsDead)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    void PlayHurtSound()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            int n = Random.Range(1, hurtSound.Length);
            audioSource.clip = hurtSound[n];
            audioSource.Play();
            hurtSound[n] = hurtSound[0];
            hurtSound[0] = audioSource.clip;
        }
    }
    void OnGUI()
    {
        healthSlider.value = healthManager.health;
    }
    IEnumerator BurnAnimate()
    {
        animator.Play("UpperBody.Hurt");
        if (!animator.GetBool("isHolding"))
        {
            animator.Play("Hands.Hurt");
        }
        PlayHurtSound();
        yield return new WaitForSeconds(burnReactivationTime);
        yield return null;
    }

    }
