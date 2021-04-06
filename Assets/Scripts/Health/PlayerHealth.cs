using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    private const float shakeDeformCoef = 0.01f;

    [SerializeField]
    private float health = 100f;
    [SerializeField]
    private float fireDamage = 10f;
    [SerializeField]
    private float burnReactivationTime = 0.5f;

    [SerializeField] private float shakeIntensity;
    [SerializeField] private float shakeDecay;
    private GameObject camera;

    private Slider healthSlider;

    private HealthManager healthManager;

    Animator animator;

    void Start()
    {
        healthSlider = GameObject.Find("HealthBar").GetComponent<Slider>();
        healthSlider.maxValue = health;
        animator = GetComponent<Animator>();

        healthManager = new HealthManager(health, fireDamage, burnReactivationTime);
        camera = GameObject.FindGameObjectWithTag("MainCamera");
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
        yield return new WaitForSeconds(burnReactivationTime);
        yield return null;
    }

    IEnumerator ShakeCamera()
    {
        Quaternion originalRot = camera.transform.rotation;
        float currentShakeIntensity = shakeIntensity;
        while (currentShakeIntensity > 0)
        {
            camera.transform.rotation = new Quaternion(
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
