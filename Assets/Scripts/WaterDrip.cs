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
                gameObject.SetActive(false);
                break;
        }
        
    }
}
