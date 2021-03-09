using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEstinguisher : MonoBehaviour
{
    private static readonly int chargeSize = 11;
    private static readonly float shootingDelay = 0.8f;

    private int chargesLeft = chargeSize;

    private bool isShooting;

    public GameObject waterPrefab;

    private HoldObject playerHoldObject;
    private void Start()
    {
        playerHoldObject = GameObject.FindGameObjectWithTag("Player").GetComponent<HoldObject>();
    }

    private void Update()
    {
        if (Input.GetKeyDown("e") && chargesLeft > 0 && !isShooting && !playerHoldObject.IsHoldingObject)
        {
            StartCoroutine(Estinguish());
        }
    }

    private IEnumerator Estinguish()
    {
        isShooting = true;

        GameObject waterDrip = Instantiate(waterPrefab, transform.position, Quaternion.identity);
        waterDrip.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.up * 5f);

        chargesLeft--;
        yield return new WaitForSeconds(shootingDelay);
        isShooting = false;
    }
}
