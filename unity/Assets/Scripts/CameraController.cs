using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this is a program that controls the motion of the camera
public class CameraController : MonoBehaviour {
    // it requires a GameObject that will be defined as the player
    public GameObject player;

    // we will define a vector called offset to house a constant location for the camera
    private Vector3 offset;

    // 
    void Start ()
    {
        offset = transform.position - player.transform.position;
    }

    void LateUpdate ()
    {
        transform.position = player.transform.position + offset;
    }
}
