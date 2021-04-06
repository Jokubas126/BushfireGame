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

    public AudioClip extinguishSound;
    private AudioSource audioSource;
    private void Start()
    {
        animator = transform.parent.GetComponent<Animator>();
        Physics.IgnoreLayerCollision(4, 10);
        Physics.IgnoreLayerCollision(4, 8);
        RefillCharges();
        playerHoldObject = GameObject.FindGameObjectWithTag("Player").GetComponent<HoldObject>();
        extinguisherSlider = GameObject.Find("ExtinguisherBar").GetComponent<Slider>();
        extinguisherSlider.maxValue = tankSize;
        audioSource = gameObject.AddComponent<AudioSource>() as AudioSource;
    }

    private void Update()
    {
        if (Input.GetKey("space") && waterLeft > 0 && !isShooting && !playerHoldObject.IsHoldingObject && isUnderPlayerControl)
        {
            StartCoroutine(Extinguish());
        }
        StopExtinguishSound();
    }

    void OnGUI()
    {
        extinguisherSlider.value = waterLeft;
    }
    void PlayExtinguishSound()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.clip = extinguishSound;
            audioSource.Play();
        }
        audioSource.volume = 0.5F;
    }
    void StopExtinguishSound()
    {
        if (!isShooting)
        {
            audioSource.Pause();
        }

    }
    private IEnumerator Extinguish()
    {
        isShooting = true;
        animator.Play("Hands.Extinguish");
        PlayExtinguishSound();
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
