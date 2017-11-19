using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCamera : MonoBehaviour {

    public Vector3 speed;     //this will be the speed in the x-direction, added by user
    public GameObject obj;    // this is the object to be contracted.

    private float lightSpeed = 100f;

    private float maximumDistance = 100f;

    void FixedUpdate()
    {   // at each time step move the camera based on the speed inserted above.
        transform.position += speed * Time.deltaTime;
        // if the x position of the object is greater than the distance, reset the poisiton.
        if (transform.position.x > maximumDistance)
            transform.position = Vector3.zero;
    }

    void Update()
    {
        Contraction();

        if (Input.GetKeyDown("space"))
            speed += new Vector3 (10,0,0);

        if (speed.magnitude > lightSpeed)
            speed = new Vector3(10, 0, 0);
    }

    // this function will be our length contraction.

    //private Vector3 minSize = Vector3.(0f, 10f, 10f);

    private Vector3 minSize = new Vector3 (0f,10f,10f);
    private Vector3 normSize = new Vector3(10f, 10f, 10f);

    void Contraction()
    {
        // Determine the current speed and calculate the gamma factor.
        float currentSpeed = speed.magnitude;
        float gamma = Mathf.Sqrt(1 - Mathf.Pow(currentSpeed / lightSpeed, 2f));

        // change the object size based on the gamma factor
        obj.transform.localScale = new Vector3 (normSize.x * gamma, normSize.y,normSize.z);

        // code below just as an emergency message incase the size ever gets negative. 
        if (obj.transform.localScale.x < minSize.x)
            obj.transform.localScale = normSize;
    }

}
