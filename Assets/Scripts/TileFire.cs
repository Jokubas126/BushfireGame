using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileFire : MonoBehaviour
{
    public int fireResistanceMax;
    public int fireResistanceCurrent; //If this is 0, tile is considered on fire
    public int fireDuration; //If we want fire to be able to die out on its own

    void Start()
    {

        //fireResistanceMax = Random.Range(5, 100); //Moved to tile initialization
        //fireResistanceCurrent = fireResistanceMax;
    }

    void Update()
    {
        /*if (fireResistanceCurrent > 0) //Moved to TileIgnition in FireController
        {
            gameObject.GetComponent<Renderer>().material.color = new Color(.219f, (float)fireResistanceCurrent / fireResistanceMax, 0);
        } else
        {
            gameObject.GetComponent<Renderer>().material.color = new Color(1, .6f, 0);
        }*/
    }
}
