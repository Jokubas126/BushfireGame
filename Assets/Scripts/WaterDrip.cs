using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDrip : MonoBehaviour
{
    public float extinguishPower = 5;

    private void Start()
    {
        Physics.IgnoreLayerCollision(0, 10);
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Grass":
            case "Bush":
            case "Tree":
            case "Water":
            case "Rock":
                if (collision.gameObject.GetComponentInParent<TileFire>() != null)
                    collision.gameObject.GetComponentInParent<TileFire>().fireResistanceCurrent += extinguishPower;
                gameObject.SetActive(false);
                break;
        }
        
    }
}
