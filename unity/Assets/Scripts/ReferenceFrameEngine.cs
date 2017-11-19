using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReferenceFrameEngine : MonoBehaviour {

    public static List<GameObject> bodies;    // this is the list of object to be contracted.
    private Vector3 velocity;  // this is the initial velocity of the object.

    private float lightSpeed = 100f; // speed of light
    public float maximumDistance = 100f; // maximum distance the player travels before resetting

    public Text speedText; // a class for adding text
    private float speedCount; // counter that will display the speed 

    private GameObject refFrame;       // Call the MainEngine
    private MainEngine refFrameEngine;

    // On start up Unity will run this code
    void Start()
    { 
        // On the start up, initialize the variables.
        refFrame = GameObject.Find("Main Camera");
        refFrameEngine = GetComponent<MainEngine>();

        // use the velocity vector determined from the Physics Engine
        velocity = refFrameEngine.velocityVector;

        // initialize the counter.
        speedCount = 10f/lightSpeed;
        SetSpeedText(); // call our UI update function.
    }

    // Add all objects with a specific Tag into our bodies list
    void OnEnable()
    {
        if (bodies == null) // if there is nothing in the list begin appending.
        {
            bodies = new List<GameObject>();
        }

        // look for the Tag "contractable" and add it.
        foreach (GameObject contractable in GameObject.FindGameObjectsWithTag("contractable"))
        {
            bodies.Add(contractable);
        }
        
    }

    void FixedUpdate()
    {   // at each time step move the camera based on the speed inserted above.
        //transform.position += speed * Time.deltaTime;
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
            velocity = (velocity + lightVector*0.9999999f)*0.5f; // update the speed with the speed of light

            refFrameEngine.setVelocity(velocity); // set the Velocity in the Physics Engine in the MainEngine.

            // update the speed counter display
            speedCount = velocity.magnitude/lightSpeed;
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
        float currentSpeed = velocity.magnitude;
        float gamma = Mathf.Sqrt(1 - Mathf.Pow(currentSpeed / lightSpeed, 2f));

        // change the object size of the objects in our bodies list based on the gamma factor
        foreach (GameObject contractable in bodies)
        { 
            contractable.transform.localScale = new Vector3(normSize.x * gamma, normSize.y, normSize.z);

            // code below just as an emergency message incase the size ever gets negative. 
            if (contractable.transform.localScale.x < minSize.x)
                contractable.transform.localScale = normSize;
        }
        
    }

    // update the speed counter display that the player will view.
    void SetSpeedText()
    {   // basically just turns everything into a string and writes it in unity.
        speedText.text = (velocity.magnitude/lightSpeed).ToString() + "c";
    }

}
