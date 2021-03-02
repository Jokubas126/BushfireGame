using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MapManager : MonoBehaviour
{
    public GameObject[,] map = new GameObject[20,20];
    public GameObject grassPrefab;
    public GameObject treePrefab;

    void Start()
    {
        GenerateMap("Assets/Resources/TestFile.txt");
    }

    void Update()
    {
        
    }

    void GenerateMap(string mapFilePath)
    {
        List<string> mapLines = ReadFile(mapFilePath);
        for (int y = 0; y < mapLines.Count; y++)
        {
            for (int x = 0; x < mapLines[y].Length; x++)
            {
                Debug.Log("Spawning tile");
                map[x, y] = spawnTile(mapLines[y][x], x, y);
            }
        }
    }

    GameObject spawnTile(char tileType, int x, int y)
    {
        Vector3 spawnLocation = new Vector3( x, 0, y );
        GameObject tileToSpawn = null;
        switch (tileType)
        {
            case 'G':
                tileToSpawn = grassPrefab;
                break;
            case 'T':
                tileToSpawn = treePrefab;
                break;
            default:
                Debug.LogError("Can't spawn undefined tile!");
                break;
        }

        return Instantiate(tileToSpawn, spawnLocation, Quaternion.identity, gameObject.transform);
    }

    List<string> ReadFile(string path)
    {
        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);
        List<string> lines = new List<string>();
        string line = reader.ReadLine();
        while (line != null)
        {
            lines.Add(line);
            line = reader.ReadLine();    //read next line
        }
        reader.Close();
        return lines;
    }
}
