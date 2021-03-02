using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.Networking;


public class MapManager : MonoBehaviour
{
    public GameObject[,] map = new GameObject[100,100]; 
    public GameObject grassPrefab;
    public GameObject treePrefab;
    public GameObject bushPrefab;
    public GameObject rockPrefab;
    public GameObject waterPrefab;

    void Start()
    {
        StartCoroutine(ReadFile());    //online level loading
    }

    void GenerateMap(string[] lines)
    {
        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines[y].Length; x++)
            {
                map[x, y] = spawnTile(lines[y][x], x, y);
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
            case 'B':
                tileToSpawn = bushPrefab;
                break;
            case 'R':
                tileToSpawn = rockPrefab;
                break;
            case 'W':
                tileToSpawn = waterPrefab;
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

    IEnumerator ReadFile()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://group-609.github.io/BushfireGame/levelData.txt");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            string[] textLines = www.downloadHandler.text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            Debug.LogError("Lines counted: " + textLines.Length);
            GenerateMap(textLines);
        }
    }
}
