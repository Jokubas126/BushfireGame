using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FireExtinguisher : MonoBehaviour
{
    public int tankSize = 100;
    public float waterDropDelay = 0.05f;
    public float chargeVelocity = 6f;
    public float extinguisherSpread = 0.8f;
    private Animator animator;
    public bool isUnderPlayerControl = true;

    private int waterLeft;

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
        extinguisherSlider.maxValue = tankSize;
    }

    private void Update()
    {
        if (Input.GetKey("space") && waterLeft > 0 && !isShooting && !playerHoldObject.IsHoldingObject && isUnderPlayerControl)
        {
            StartCoroutine(Extinguish());
        }
    }

    void OnGUI()
    {
        extinguisherSlider.value = waterLeft;
    }

    private IEnumerator Extinguish()
    {
        isShooting = true;
        animator.Play("Hands.Extinguish");
        GameObject waterDrip = Instantiate(waterPrefab, transform.position, Quaternion.identity);
        waterDrip.GetComponent<Rigidbody>().velocity = transform.TransformDirection(
            new Vector3(Random.Range(-extinguisherSpread, extinguisherSpread), 1, Random.Range(-extinguisherSpread, extinguisherSpread)) * chargeVelocity
        );
        waterLeft--;
        yield return new WaitForSeconds(waterDropDelay);
        isShooting = false;
    }

    public void RefillCharges()
    {
        waterLeft = tankSize;
    }
}
