using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    private GameObject player;
    public float cameraHeight = 10.0f;
    public float cameraDistance = -21.0f;

    void Start()
    {
        this.player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        Vector3 pos = player.transform.position;
        pos.y += cameraHeight;
        pos.z += cameraDistance;
        transform.position = pos;
        transform.LookAt(player.transform);
    }
}