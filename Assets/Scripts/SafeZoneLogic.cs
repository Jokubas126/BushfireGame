using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SafeZoneLogic : MonoBehaviour
{
    private GameObject player;
    private GameObject waterHose;
    [SerializeField]
    private int radius;
    public List<GameObject> animalsAlive;
    public int returnedPickables;

    private bool objectsFound = false;

    void Start()
    {
        StartCoroutine(LocateObjects());
    }

    void Update()
    {
        if (objectsFound == true)
        {
            animalsAlive = GameObject.FindGameObjectsWithTag("PickableObject").ToList();

            if (player.GetComponent<HoldObject>().IsHoldingObject == false && IsTargetInRange(player, radius))
            {
                waterHose.GetComponent<FireExtinguisher>().RefillCharges();
            }

            returnedPickables = animalsAlive.FindAll(
                    delegate (GameObject animal)
                    {
                        return IsTargetInRange(animal, radius);
                    }
                ).Count;

            if (returnedPickables == animalsAlive.Count)
            {
                GameObject.Find("Canvas").transform.Find("WinText").gameObject.SetActive(true);
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

    private IEnumerator LocateObjects() //All objects will have to have spawned before the safetyzone can locate them. Better solution will be to run a function from mapmanager when it's done loading all tiles.
    {
        yield return new WaitForSeconds(2);
        objectsFound = true;
        player = GameObject.FindGameObjectWithTag("Player");
        waterHose = player.transform.Find("WaterHose").gameObject;
        GameObject.Find("Canvas").transform.Find("Score").gameObject.SetActive(true);
    }
}
