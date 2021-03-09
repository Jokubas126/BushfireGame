using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    public GameObject tile;
    public GameObject[,] map; //collect from mapmanager later

    public int fireTickDelay = 1000;
    public int fireTickCounter;
    private int mapSizeX;
    private int mapSizeY;

    void Start()
    {
        map = transform.GetComponent<MapManager>().map;
        mapSizeX = transform.GetComponent<MapManager>().mapSizeX;
        mapSizeY = transform.GetComponent<MapManager>().mapSizeY;


        for (int i = 0; i < mapSizeX; i++)
        {
            for (int j = 0; j < mapSizeY; j++)
            {
                map[i, j].GetComponent<TileFire>().fireResistanceMax += Random.Range(5, 20);       //Add randomness to fire resistance
                map[i, j].GetComponent<TileFire>().fireResistanceCurrent = map[i, j].GetComponent<TileFire>().fireResistanceMax;
                if (map[i, j].tag == "Rock" || map[i, j].tag == "Water")  //We dont burn water and rock tiles
                {
                    break;
                }
                //map[i, j].GetComponent<Renderer>().material.color = new Color(.219f, 1, 0);  //Temp all tile start color
            }
        }

        fireTickCounter = fireTickDelay;

        map[mapSizeX / 2, mapSizeY / 2].GetComponent<TileFire>().fireResistanceCurrent = 0; //Just temp starting location
        //map[map.GetLength(0) / 2, map.GetLength(1) / 2].GetComponent<Renderer>().material.color = new Color(1, .6f, 0); //Temp start loc color
    }

    void FixedUpdate()
    {
        fireTickCounter--;
        if (fireTickCounter <= 0)
        {
            for (int i = 0; i < mapSizeX; i++)
            {
                for (int j = 0; j < mapSizeY; j++)
                {
                    if (map[i, j].GetComponent<TileFire>().fireResistanceCurrent == 0)
                    {
                        if(i > 0)
                        {
                            TileIgnition(i - 1, j);
                        }
                        if(i < mapSizeX)
                        {
                            TileIgnition(i + 1, j);
                        }
                        if(j > 0)
                        {
                            TileIgnition(i, j - 1);
                        }
                        if (j < mapSizeY)
                        {
                            TileIgnition(i, j + 1);
                        }
                    }
                }
            }
            fireTickCounter = fireTickDelay;
        }
    }

    private void TileIgnition(int i, int j)
    {
        if (gameObject.tag == "Rock" || gameObject.tag == "Water")  //We dont burn water and rock tiles
        { 
            return;
        }
        else if (map[i, j].GetComponent<TileFire>().fireResistanceCurrent > 0)
        { 
            map[i, j].GetComponent<TileFire>().fireResistanceCurrent -= 1;

            if (map[i, j].GetComponent<TileFire>().fireResistanceCurrent > 0) //Color assign here to only run this every fireTickDelay. Color is just temporary. will be visuals based on <75, <50, <25, etc in future instead
            {
                //map[i, j].GetComponent<Renderer>().material.color = new Color(.219f, (float)map[i, j].GetComponent<TileFire>().fireResistanceCurrent / map[i, j].GetComponent<TileFire>().fireResistanceMax, 0);
            }
            else
            {
                //map[i, j].GetComponent<Renderer>().material.color = new Color(1, .6f, 0);
            }
        }
    }
}
