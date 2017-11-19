using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moveCamera : MonoBehaviour {

    public Vector3 speed = new Vector3(10f,0f,0f);     //this will be the speed in the x-direction, added by user
    public GameObject obj;    // this is the object to be contracted.

    private float lightSpeed = 100f; // speed of light
    public float maximumDistance = 100f; // maximum distance the player travels before resetting

    public Text speedText; // a class for adding text
    private float speedCount; // counter that will display the speed 

    // On start up Unity will run this code
    void Start()
    {   // initialize the counter.
        speedCount = 10f/lightSpeed;
        SetSpeedText(); // call our UI update function.
    }

    void FixedUpdate()
    {   // at each time step move the camera based on the speed inserted above.
        transform.position += speed * Time.deltaTime;
        // if the x position of the object is greater than the distance, reset the poisiton.
        if (transform.position.x > maximumDistance)
            transform.position = Vector3.zero;
    }

    // this is the update loop which works similarly to the FixedUpdate loop
    void Update()
    {   // calls the contraction function from below.

        Contraction();
        
        // if the space bar is pressed down by the player do the following:
        if (Input.GetKeyDown("space"))
        {   
            Vector3 lightVector = new Vector3(lightSpeed, 0, 0); // initialize a light Vector.
            speed = (speed + lightVector*0.9999999f)*0.5f; // update the speed with the speed of light
            
            // update the speed counter display
            speedCount = speed.magnitude/lightSpeed;
            SetSpeedText();
        }    
    }
    // initialize some size limitations for our object.
    // Yes, Vince, I know this is not good for general coding.
    private Vector3 minSize = new Vector3(0f, 10f, 10f);
    private Vector3 normSize = new Vector3(10f, 10f, 10f);

    // this function will be our length contraction 
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

    // update the speed counter display that the player will view.
    void SetSpeedText()
    {   // basically just turns everything into a string and writes it in unity.
        speedText.text = (speed.magnitude/lightSpeed).ToString() + "c";
    }

}
