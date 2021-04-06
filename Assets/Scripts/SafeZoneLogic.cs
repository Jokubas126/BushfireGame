using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SafeZoneLogic : MonoBehaviour
{
    private GameObject player;
    private GameObject waterHose;
    private GameObject canvas;
    [SerializeField]
    private int radius;
    public List<GameObject> animalsAlive;
    public int animalsSaved;

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

            animalsSaved = animalsAlive.FindAll(
                    delegate (GameObject animal)
                    {
                        return IsTargetInRange(animal, radius);
                    }
                ).Count;

            canvas.transform.Find("WinText").gameObject.SetActive(animalsSaved == animalsAlive.Count && IsTargetInRange(player, radius));
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
        yield return new WaitForSeconds(1);
        objectsFound = true;
        player = GameObject.FindGameObjectWithTag("Player");
        waterHose = player.transform.Find("WaterHose").gameObject;
        animalsAlive = GameObject.FindGameObjectsWithTag("PickableObject").ToList();
        Score scoreScript = GameObject.Find("Canvas").GetComponent<Score>();
        scoreScript.enabled = true;
        scoreScript.animalsAtStart = animalsAlive.Count;
        canvas = GameObject.Find("Canvas");
    }
}
