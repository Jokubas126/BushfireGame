using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    public GameObject player;
    public float cameraHeight = 10.0f;
    public float cameraDistance = -21.0f;

    void Update()
    {
        Vector3 pos = player.transform.position;
        pos.y += cameraHeight;
        pos.z += cameraDistance;
        transform.position = pos;
        transform.LookAt(player.transform);
    }
}