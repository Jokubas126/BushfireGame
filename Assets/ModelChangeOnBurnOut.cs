using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelChangeOnBurnOut : MonoBehaviour
{
    public TileFire tileFire;
    public GameObject fromModel;
    public GameObject toModel;

    void Start()
    {
        
    }

    void Update()
    {
        if (tileFire.fireResistanceCurrent > 0 && tileFire.fireDuration <= 0)
        {
            fromModel.SetActive(false);
            toModel.SetActive(true);
        }
    }
}
