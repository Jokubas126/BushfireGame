using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZoneLogic : MonoBehaviour
{
    private GameObject player;
    private GameObject waterHose;
    [SerializeField]
    private int radius;
    private GameObject[] pickables;
    private int returnedPickables;

    private bool objectsFound = false;

    void Start()
    {
        StartCoroutine(LocateObjects());
    }

    void Update()
    {
        if (objectsFound == true)
        {
            if (player.GetComponent<HoldObject>().IsHoldingObject == false && IsTargetInRange(player, radius))
            {
                waterHose.GetComponent<FireExtinguisher>().RefillCharges();
            }

            returnedPickables = 0;
            for (int i = 0; i < pickables.Length; i++)
            {
                if (IsTargetInRange(pickables[i], radius))
                {
                    returnedPickables++;
                }
            }

            if (returnedPickables == pickables.Length)
            {
                //Win condition
                Debug.Log("Win");
            }
        }
    }

    private bool IsTargetInRange(GameObject target, int closeRange)
    {
        float dist = Vector3.Distance(target.transform.position, gameObject.transform.position);
        if (dist <= closeRange)
        {
            return true;
        }
        return false;
    }

    private IEnumerator LocateObjects()
    {
        yield return new WaitForSeconds(2);
        objectsFound = true;
        player = GameObject.FindGameObjectWithTag("Player");
        waterHose = player.transform.Find("WaterHose").gameObject;
        pickables = GameObject.FindGameObjectsWithTag("PickableObject");
    }
}
