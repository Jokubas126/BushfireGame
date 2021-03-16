using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZoneLogic : MonoBehaviour
{ 
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private int radius;
    private GameObject[] pickables;
    private int returnedPickables;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pickables = GameObject.FindGameObjectsWithTag("PickableObject");
    }

    void Update()
    {
        if (player.GetComponent<HoldObject>().GetPickedUpObject() == null && IsTargetInRange(player, radius))
        {
            //Add extinguish ammo
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

    private bool IsTargetInRange(GameObject target, int closeRange)
    {
        float dist = Vector3.Distance(target.transform.position, gameObject.transform.position);
        if (dist <= closeRange)
        {
            return true;
        }
        return false;
    }
}
