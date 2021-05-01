using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    public GameObject animalBadgePrefab;
    private int animalsDead = 0;
    private int animalsSaved = 0;
    private int animalsTotal;

    private Transform animalBadgeContainer;
    public Sprite aliveKoalaSprite;
    public Sprite savedKoalaSprite;
    public Sprite deadKoalaSprite;

    public void LoadAnimalsAtStart(int animalsAtStart)
    {
        animalsTotal = animalsAtStart;
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
        animalsDead++;
    }

    public void AnimalsIconStatusUpdate(int amount)
    {
        animalsSaved = amount;

        for (int i = 0; i < animalsTotal; i++)
        {
            animalBadgeContainer.GetChild(i).GetComponent<Image>().sprite = aliveKoalaSprite;
        }

        for (int i = 0; i < animalsDead; i++)
        {
            animalBadgeContainer.GetChild(i).GetComponent<Image>().sprite = deadKoalaSprite;
        }

        for (int i = animalsDead; i < animalsDead + animalsSaved; i++)
        {
            animalBadgeContainer.GetChild(i).GetComponent<Image>().sprite = savedKoalaSprite;
        }
        
    }
}
