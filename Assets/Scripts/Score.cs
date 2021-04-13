using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    public GameObject animalBadgePrefab;
    private int animalsDead = 0;

    private Transform animalBadgeContainer;
    public Sprite aliveKoalaSprite;
    public Sprite deadKoalaSprite;

    public void LoadAnimalsAtStart(int animalsAtStart)
    {
        animalBadgeContainer = transform.Find("Animal Badges");
        for(int i=0; i<animalsAtStart; i++)
        {
            GameObject badge = Instantiate(animalBadgePrefab, animalBadgeContainer);
            badge.GetComponent<Image>().sprite = aliveKoalaSprite;
            RectTransform rectTransform = badge.GetComponent<RectTransform>();
            rectTransform.position = new Vector2(rectTransform.position.x + rectTransform.rect.width * i, rectTransform.position.y);
        }
    }

    public void AnimalDied()
    {
        animalBadgeContainer.GetChild(animalsDead).GetComponent<Image>().sprite = deadKoalaSprite;
        animalsDead++;
    }
}
