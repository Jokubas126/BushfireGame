using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public void LoadTutorial()
    {
        Debug.Log("Loading tutorial");
        SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);
    }

    public void LoadLevel1()
    {
        Debug.Log("Loading level 1");
        SceneManager.LoadScene("Master", LoadSceneMode.Single);
    }
}
