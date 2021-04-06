using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FireExtinguisher : MonoBehaviour
{
    public int singleChargeSize = 10;
    public int chargeSize = 11;
    public float shootingDelay = 0.8f;
    public float chargeVelocity = 6f;
    public float extinguisherSpread = 0.8f;
    private Animator animator;
    public bool isUnderPlayerControl = true;

    private int chargesLeft;

    private bool isShooting;

    public GameObject waterPrefab;
    private HoldObject playerHoldObject;

    private Slider extinguisherSlider;

    private void Start()
    {
        animator = transform.parent.GetComponent<Animator>();
        Physics.IgnoreLayerCollision(4, 10);
        Physics.IgnoreLayerCollision(4, 8);
        RefillCharges();
        playerHoldObject = GameObject.FindGameObjectWithTag("Player").GetComponent<HoldObject>();
        extinguisherSlider = GameObject.Find("ExtinguisherBar").GetComponent<Slider>();
        extinguisherSlider.maxValue = chargeSize;
    }

    private void Update()
    {
        if (Input.GetKeyDown("space") && chargesLeft > 0 && !isShooting && !playerHoldObject.IsHoldingObject && isUnderPlayerControl)
        {
            StartCoroutine(Extinguish());
        }
    }

    void OnGUI()
    {
        extinguisherSlider.value = chargesLeft;
    }

    private IEnumerator Extinguish()
    {
        isShooting = true;
        animator.Play("Hands.Extinguish");
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
