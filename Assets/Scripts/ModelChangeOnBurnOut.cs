using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelChangeOnBurnOut : MonoBehaviour
{
    private TileFire tileFire;
    public GameObject fromModel;
    public GameObject toModel;
    private bool hasSwitched = false;

    void Start()
    {
        tileFire = gameObject.GetComponent<TileFire>();
    }

    void Update()
    {

        if (!hasSwitched && tileFire.fireResistanceCurrent <= 0 && tileFire.fireDuration <= 0)
        {
            fromModel.SetActive(false);
            toModel.SetActive(true);
            hasSwitched = true;
        }
    }
}
