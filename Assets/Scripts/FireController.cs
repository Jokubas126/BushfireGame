using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    public GameObject tile;
    public GameObject[,] map = new GameObject[20, 20]; //collect from mapmanager later

    public int fireTickDelay = 1000;
    public int fireTickCounter;

    void Start()
    {
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                map[i, j] = Instantiate(tile, new Vector3(i, 0, j), Quaternion.identity) as GameObject;

                map[i, j].GetComponent<TileFire>().fireResistanceMax = Random.Range(5, 100);
                map[i, j].GetComponent<TileFire>().fireResistanceCurrent = map[i, j].GetComponent<TileFire>().fireResistanceMax;
                map[i, j].GetComponent<Renderer>().material.color = new Color(.219f, 1, 0);  //Temp all tile start color
            }
        }

        fireTickCounter = fireTickDelay;

        map[map.GetLength(0) / 2, map.GetLength(1) / 2].GetComponent<TileFire>().fireResistanceCurrent = 0; //Just temp starting location
        map[map.GetLength(0) / 2, map.GetLength(1) / 2].GetComponent<Renderer>().material.color = new Color(1, .6f, 0); //Temp start loc color
    }

    void FixedUpdate()
    {
        fireTickCounter--;
        if (fireTickCounter <= 0)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j].GetComponent<TileFire>().fireResistanceCurrent == 0)
                    {
                        TileIgnition(i - 1, j);
                        TileIgnition(i + 1, j);
                        TileIgnition(i, j - 1);
                        TileIgnition(i, j + 1);
                    }
                }
            }

            fireTickCounter = fireTickDelay;
        }
    }

    private void TileIgnition(int i, int j)
    {
        if (i >= 0 && map[i, j].GetComponent<TileFire>().fireResistanceCurrent > 0)
        {
            map[i, j].GetComponent<TileFire>().fireResistanceCurrent -= 1;

            if (map[i, j].GetComponent<TileFire>().fireResistanceCurrent > 0) //Color assign here to only run this every fireTickDelay. Color is just temporary. will be visuals based on <75, <50, <25, etc in future instead
            {
                map[i, j].GetComponent<Renderer>().material.color = new Color(.219f, (float)map[i, j].GetComponent<TileFire>().fireResistanceCurrent / map[i, j].GetComponent<TileFire>().fireResistanceMax, 0);
            }
            else
            {
                map[i, j].GetComponent<Renderer>().material.color = new Color(1, .6f, 0);
            }
        }
    }
}
