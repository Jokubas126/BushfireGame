using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    public GameObject[,] map; //collect from mapmanager later

    public int fireTickDelay = 50;
    public int fireTickCounter;
    public float fireStartResistanceMultiplier = 0.1f;

    public int mapSizeX;
    public int mapSizeY;

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
                map[i, j].GetComponent<TileFire>().fireDuration = map[i, j].GetComponent<TileFire>().fireResistanceMax * 10 * Random.Range(.7f, 1f); //Randomness to duration
                if (!map[i, j].GetComponent<TileFire>().isFireStartTile)
                {
                    map[i, j].GetComponent<TileFire>().fireResistanceCurrent = map[i, j].GetComponent<TileFire>().fireResistanceMax * fireStartResistanceMultiplier;
                }
            }
        }

        fireTickCounter = fireTickDelay;
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
                    TileFire tileFire = map[i, j].GetComponent<TileFire>();
                    if (tileFire.IsTileOnFire())
                    {
                        if (i > 0)
                        {
                            TileIgnition(i - 1, j);
                        }
                        if (i < mapSizeX - 1)
                        {
                            TileIgnition(i + 1, j);
                        }
                        if (j > 0)
                        {
                            TileIgnition(i, j - 1);
                        }
                        if (j < mapSizeY - 1)
                        {
                            TileIgnition(i, j + 1);
                        }

                        tileFire.fireDuration--;
                    }
                }
                fireTickCounter = fireTickDelay;
            }
        }
    }

    private void TileIgnition(int i, int j)
    {
        if (map[i, j].tag == "Water" || map[i, j].GetComponent<TileFire>().fireDuration <= 0)  //We dont burn water tiles
        { 
            return;
        }
        else if (map[i, j].GetComponent<TileFire>().fireResistanceCurrent > 0)
        { 
            map[i, j].GetComponent<TileFire>().fireResistanceCurrent--;
        }
    }
}
