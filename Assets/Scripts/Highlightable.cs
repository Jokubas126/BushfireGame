using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlightable : MonoBehaviour
{
    //public bool highlighted = false;
    private Color originalColor;
    private Color adjustedColor;
    private bool highlighted = false;
    [SerializeField]
    private float saturationMulitplier = 0.8f;
    [SerializeField]
    private float valueMultiplier = 1.2f;

    void Start()
    {
        originalColor = GetComponent<Renderer>().material.color;
        float H, S, V;
        Color.RGBToHSV(originalColor, out H, out S, out V);
        S = S * saturationMulitplier;
        V = V * valueMultiplier;
        adjustedColor = Color.HSVToRGB(H, S, V);
    }

    void Update()
    {
        if(highlighted)
        {
            GetComponent<Renderer>().material.color = adjustedColor;
            highlighted = false;
        }
        else
        {
            GetComponent<Renderer>().material.color = originalColor;
        }
    }

    public void Highlight()
    {
        highlighted = true;
    }
}
