using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCamera : MonoBehaviour {

    public float speed = 0f;     //this will be the speed in the x-direction, added by user


    void FixedUpdate()
    {   // at each time step move the camera based on the speed inserted above.
        transform.position.x = speed * Time.deltaTime;
    }
}
