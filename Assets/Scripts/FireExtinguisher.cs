using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FireExtinguisher : MonoBehaviour
{
    public int singleChargeSize = 7;
    public int chargeSize = 11;
    public float shootingDelay = 0.8f;
    public float chargeVelocity = 4f;
    public float extinguisherSpread = 0.5f;

    private int chargesLeft;

    private bool isShooting;

    public GameObject waterPrefab;
    private HoldObject playerHoldObject;

    private Slider extinguisherSlider;

    private void Start()
    {
        RefillCharges();
        playerHoldObject = GameObject.FindGameObjectWithTag("Player").GetComponent<HoldObject>();
        extinguisherSlider = GameObject.Find("ExtinguisherBar").GetComponent<Slider>();
        extinguisherSlider.maxValue = chargeSize;
    }

    private void Update()
    {
        UpdateGUI();
        if (Input.GetKeyDown("e") && chargesLeft > 0 && !isShooting && !playerHoldObject.IsHoldingObject)
        {
            StartCoroutine(Extinguish());
        }
    }

    private void UpdateGUI()
    {
        extinguisherSlider.value = chargesLeft;
    }

    private IEnumerator Extinguish()
    {
        isShooting = true;

        for (int i = 0; i < singleChargeSize; i++)
        {
            yield return new WaitForSeconds(0.05f);
            GameObject waterDrip = Instantiate(waterPrefab, transform.position, Quaternion.identity);
            waterDrip.GetComponent<Rigidbody>().velocity = transform.TransformDirection(new Vector3(Random.Range(-extinguisherSpread, extinguisherSpread), 1, Random.Range(-extinguisherSpread, extinguisherSpread)) * chargeVelocity);
        }

        chargesLeft--;
        yield return new WaitForSeconds(shootingDelay);
        isShooting = false;
    }

    public void RefillCharges()
    {
        chargesLeft = chargeSize;
    }
}
