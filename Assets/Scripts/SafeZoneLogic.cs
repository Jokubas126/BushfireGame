using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class SafeZoneLogic : MonoBehaviour
{
    private GameObject player;
    private GameObject waterHose;
    private GameObject winText;
    private Score scoreScript;

    public int radius;
    public List<GameObject> animalsAlive;
    public int animalsSaved;

    private bool objectsFound = false;

    private bool hasPlayerWon;

    void Start()
    {
        StartCoroutine(LocateObjects());
    }

    void Update()
    {
        if (objectsFound && !hasPlayerWon)
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

            if (animalsSaved == animalsAlive.Count && IsTargetInRange(player, radius))
            {
                winText.SetActive(true);
                hasPlayerWon = true;
                StartCoroutine(ReturnToLevelSelect());
            }
        }
    }

    private IEnumerator ReturnToLevelSelect()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("LevelSelect", LoadSceneMode.Single);
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

    //All objects will have to have spawned before the safetyzone can locate them. 
    //Better solution will be to run a function from mapmanager when it's done loading all tiles.
    private IEnumerator LocateObjects()
    {
        yield return new WaitForSeconds(1);
        player = GameObject.FindGameObjectWithTag("Player");
        waterHose = player.transform.Find("WaterHose").gameObject;
        animalsAlive = GameObject.FindGameObjectsWithTag("PickableObject").ToList();
        scoreScript = GameObject.Find("Canvas").GetComponent<Score>();
        scoreScript.enabled = true;
        scoreScript.LoadAnimalsAtStart(animalsAlive.Count);
        winText = GameObject.Find("Canvas").transform.Find("WinText").gameObject;
        objectsFound = true;
    }
}
