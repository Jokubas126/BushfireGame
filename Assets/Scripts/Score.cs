using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public TMP_Text animalsSavedTextMesh;
    public TMP_Text animalsDeadTextMesh;
    private int animalsDead;
    private SafeZoneLogic safeZone;
    private int animalsAtStart;

    void Start()
    {
        transform.Find("Animals saved").gameObject.SetActive(true);
        transform.Find("Animals dead").gameObject.SetActive(true);
        safeZone = GameObject.Find("MapManager").transform.Find("SafetyZone(Clone)").GetComponent<SafeZoneLogic>();
        animalsAtStart = safeZone.animalsAlive.Count;
    }

    void Update()
    {
        animalsSavedTextMesh.SetText("Animals saved: {0}/{1} ", safeZone.animalsSaved, animalsAtStart);
    }

    public void AnimalDied()
    {
        animalsDead++;
        animalsDeadTextMesh.SetText("Animals lost: {0}", animalsDead);
    }
}
