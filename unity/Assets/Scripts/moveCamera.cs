using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCamera : MonoBehaviour {

    public Vector3 speed;     //this will be the speed in the x-direction, added by user


    void FixedUpdate()
    {
        Vector3 transform.position = speed / Time.deltaTime;
    }
}
