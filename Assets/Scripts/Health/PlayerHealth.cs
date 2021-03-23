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

    private Slider healthSlider;

    private HealthManager healthManager;

    Animator animator;

    void Start()
    {
        healthSlider = GameObject.Find("HealthBar").GetComponent<Slider>();
        healthSlider.maxValue = health;
        animator = GetComponent<Animator>();

        healthManager = new HealthManager(health, fireDamage, burnReactivationTime);
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

    }
