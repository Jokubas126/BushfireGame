using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{

    private AudioClip[] hurtSound = new AudioClip[0];
    private AudioSource audioSource;

    private const float shakeDeformCoef = 0.01f;

    public float health = 100f;
    public float fireDamage = 10f;
    public float burnReactivationTime = 0.5f;

    public float shakeIntensity;
    public float shakeDecay;
    private GameObject mainCamera;


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
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

    }

    void Update()
    {
        if (healthManager.IsAllowedToBurn(transform.position))
        {
            StartCoroutine(healthManager.Burn());
            StartCoroutine(BurnAnimate());
            StartCoroutine(ShakeCamera());
        }
        if (healthManager.IsDead)
        {
            SceneManager.LoadScene("LevelSelect", LoadSceneMode.Single);
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

    IEnumerator ShakeCamera()
    {
        Quaternion originalRot = mainCamera.transform.rotation;
        float currentShakeIntensity = shakeIntensity;
        while (currentShakeIntensity > 0)
        {
            mainCamera.transform.rotation = new Quaternion(
               GetDeformedRotation(originalRot.x, currentShakeIntensity),
               GetDeformedRotation(originalRot.y, currentShakeIntensity),
               GetDeformedRotation(originalRot.z, currentShakeIntensity),
               GetDeformedRotation(originalRot.w, currentShakeIntensity)
            );

            currentShakeIntensity -= shakeDecay * Time.deltaTime;
            yield return null;
        }
    }

    private float GetDeformedRotation(float axisValue, float currentShakeIntensity)
    {
        return axisValue + Random.Range(-currentShakeIntensity, currentShakeIntensity) * shakeDeformCoef;
    }
}
