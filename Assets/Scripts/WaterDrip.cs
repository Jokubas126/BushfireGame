using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDrip : MonoBehaviour
{

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
                    collision.gameObject.GetComponentInParent<TileFire>().fireResistanceCurrent += 5;
                gameObject.SetActive(false);
                break;
        }
        
    }
}
