using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public TMP_Text textmeshPro = null;
    int animalsSaved;
    int animalsAlive;
    SafeZoneLogic safeZone;

    void Start()
    {
        safeZone = GameObject.Find("MapManager").transform.Find("SafetyZone(Clone)").GetComponent<SafeZoneLogic>();
    }

    // Update is called once per frame
    void Update()
    {

        animalsSaved = safeZone.returnedPickables;
        animalsAlive = safeZone.animalsAlive.Count;
        textmeshPro.SetText("Animals saved: {0}/{1} ", animalsSaved, animalsAlive);
    }

}
