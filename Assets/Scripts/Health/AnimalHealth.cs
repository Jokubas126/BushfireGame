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

    void Start()
    {
        healthManager = new HealthManager(health, fireDamage, burnReactivationTime);
    }

    void Update()
    {
        if (healthManager.IsAllowedToBurn(transform.position))
        {
            StartCoroutine(healthManager.Burn());
        }
        if (healthManager.IsDead)
        {
            Debug.Log("Koala is dead");
            Destroy(gameObject);
        }
    }
}
