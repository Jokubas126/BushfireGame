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
    public GameObject borderWallPrefab;
    public GameObject koalaPrefab;
    public GameObject playerPrefab;
    public GameObject cameraPrefab;
    public int mapSizeX = 0;
    public int mapSizeY = 0;

    [SerializeField]
    private string levelFileName = "";

    void Start()
    {
        StartCoroutine(LoadLevel());    //online level loading
    }

    void GenerateMap(string[] lines)
    {
        mapSizeX = lines[0].Length;
        mapSizeY = lines.Length;
        for (int y = 0; y < mapSizeY; y++)
        {
            for (int x = 0; x < mapSizeX; x++)
            {
                map[x, y] = spawnTile(lines[y][x], x, y);
            }
        }
        Instantiate(cameraPrefab, new Vector3(0,0,0), Quaternion.identity, gameObject.transform);
    }

    GameObject spawnTile(char tileType, int x, int y)
    {
        Vector3 spawnLocation = new Vector3( x, 0, y );
        GameObject tileToSpawn = null;
        GameObject spawnedTile;
        switch (tileType)
        {
            case 'G': 
            case 'g':
                tileToSpawn = grassPrefab;
                break;
            case 'T': 
            case 't':
                tileToSpawn = treePrefab;
                break;
            case 'B': 
            case 'b':
                tileToSpawn = bushPrefab;
                break;
            case 'R':
                tileToSpawn = rockPrefab;
                break;
            case 'W':
                tileToSpawn = waterPrefab;
                break;
            case 'K':       //koala
                tileToSpawn = grassPrefab;
                Instantiate(koalaPrefab, spawnLocation + new Vector3(0,1,0), Quaternion.identity, gameObject.transform);
                break;
            case 'P':       //player
                tileToSpawn = grassPrefab;
                Instantiate(playerPrefab, spawnLocation + new Vector3(0, 1, 0), Quaternion.identity, gameObject.transform);
                break;
            case 'S':       //Safezone
                tileToSpawn = grassPrefab;
                //Instantiate(safezonePrefab, spawnLocation + new Vector3(0, 1, 0), Quaternion.identity, gameObject.transform);     //uncomment when merging with Mikkel's safezone implementation
                break;
            default:
                Debug.LogError("Can't spawn undefined tile!: " + tileType);
                break;
        }
        spawnedTile = Instantiate(tileToSpawn, spawnLocation, Quaternion.identity, gameObject.transform);
        if(Char.IsLower(tileType)) //lowercase charecters mean tile should start on fire
        {
            spawnedTile.GetComponent<TileFire>().isFireStartTile = true;
        }
        return spawnedTile;
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

    IEnumerator LoadLevel()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://group-609.github.io/BushfireGame/"+ levelFileName + ".txt");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError(www.error);
        }
        else
        {
            // Show results as text
            string[] textLines = www.downloadHandler.text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            GenerateMap(textLines);
            transform.GetComponent<FireController>().enabled = true;
            CreateWallAroundMap();
        }
    }

    private void CreateWallAroundMap()
    {
        for(int i = 0; i < mapSizeX; i++)
        {
            Vector3 spawnLocation = new Vector3(i, 0, mapSizeY);
            Instantiate(borderWallPrefab, spawnLocation, Quaternion.identity, gameObject.transform);
            spawnLocation = new Vector3(i, 0, -1);
            Instantiate(borderWallPrefab, spawnLocation, Quaternion.identity, gameObject.transform);
        }
        for (int i = 0; i < mapSizeY; i++)
        {
            Vector3 spawnLocation = new Vector3(mapSizeX, 0, i);
            Instantiate(borderWallPrefab, spawnLocation, Quaternion.identity, gameObject.transform);
            spawnLocation = new Vector3(-1, 0, i);
            Instantiate(borderWallPrefab, spawnLocation, Quaternion.identity, gameObject.transform);
        }
    }
}
