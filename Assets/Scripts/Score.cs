using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public TMP_Text animalsSavedTextMesh;
    public TMP_Text animalsDeadTextMesh;
    int animalsSaved;
    int animalsAlive;
    int animalsDead;
    SafeZoneLogic safeZone;

    void Start()
    {
        safeZone = GameObject.Find("MapManager").transform.Find("SafetyZone(Clone)").GetComponent<SafeZoneLogic>();
    }

    void Update()
    {
        animalsSaved = safeZone.returnedPickables;
        animalsAlive = safeZone.animalsAlive.Count;
        animalsSavedTextMesh.SetText("Animals saved: {0}/{1} ", animalsSaved, animalsAlive);
    }

    public void AnimalDied()
    {
        animalsDead++;
        animalsDeadTextMesh.SetText("Animals dead: {0}", animalsDead);
    }
}
