using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDrip : MonoBehaviour
{
    public float estinguishPower = 5;

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
                    collision.gameObject.GetComponentInParent<TileFire>().fireResistanceCurrent += estinguishPower;
                gameObject.SetActive(false);
                break;
        }
        
    }
}
