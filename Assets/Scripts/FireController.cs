using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    public GameObject[,] map; //collect from mapmanager later

    public int fireTickDelay = 50;
    public int fireTickCounter;

    public int[] fireStartX;
    public int[] fireStartZ;

    private int mapSizeX;
    private int mapSizeY;

    void Start()
    {
        MapManager mapManager = transform.GetComponent<MapManager>();
        map = mapManager.map;
        mapSizeX = mapManager.mapSizeX;
        mapSizeY = mapManager.mapSizeY;


        for (int i = 0; i < mapSizeX; i++)
        {
            for (int j = 0; j < mapSizeY; j++)
            {
                map[i, j].GetComponent<TileFire>().fireResistanceMax += Random.Range(5, 20);       //Add randomness to fire resistance
                map[i, j].GetComponent<TileFire>().fireResistanceCurrent = map[i, j].GetComponent<TileFire>().fireResistanceMax;
            }
        }

        fireTickCounter = fireTickDelay;

        for(int i = 0; i < fireStartX.Length; i++)
        {
            map[fireStartX[i], fireStartZ[i]].GetComponent<TileFire>().fireResistanceCurrent = 0;
        }
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
                        if(i < mapSizeX - 1)
                        {
                            TileIgnition(i + 1, j);
                        }
                        if(j > 0)
                        {
                            TileIgnition(i, j - 1);
                        }
                        if (j < mapSizeY - 1)
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
        }
    }
}
