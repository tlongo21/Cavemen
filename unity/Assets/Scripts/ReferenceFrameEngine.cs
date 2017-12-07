using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReferenceFrameEngine : MonoBehaviour {

    public static List<GameObject> bodies;    // This is the list of object to be contracted.
    private Vector3 velocity;  // This is the initial velocity of the object.
    private Vector3 acceleration;

    private float lightSpeed = 100f; // Speed of light
    public float maximumDistance = 100f; // Maximum distance the player travels before resetting

    public Text speedText; // A class for adding text
    private float speedCount; // Counter that will display the speed

    private GameObject refFrame;       // Call the MainEngine
    private MainEngine refFrameEngine;

    // On start up Unity will run this code
    void Start()
    {
        // On the start up, initialize the variables.
        refFrame = GameObject.Find("Main Camera");
        refFrameEngine = GetComponent<MainEngine>();

        // Use the velocity vector determined from the Physics Engine
        velocity = refFrameEngine.velocityVector;
        //acceleration = refFrameEngine.accelerationVector;

        // Initialize the counter.
        speedCount = 10f/lightSpeed;
        SetSpeedText(); // Call our UI update function.
    }

    // Add all objects with a specific Tag into our bodies list
    void OnEnable()
    {
        if (bodies == null) // If there is nothing in the list begin appending.
        {
            bodies = new List<GameObject>();
        }

        // Look for the Tag "contractable" and add it.
        foreach (GameObject contractable in GameObject.FindGameObjectsWithTag("contractable"))
        {
            bodies.Add(contractable);
        }

    }

    void FixedUpdate()
    {
        // If the x position of the object is greater than the distance, reset the poisiton.
        if (transform.position.x > maximumDistance)
            transform.position = Vector3.zero;
        if (transform.position.x < 0f)
            transform.position = new Vector3(1,0,0)*maximumDistance;
    }

    // This is the update loop which works similarly to the FixedUpdate loop
    void Update()
    {   // Calls the contraction function from below.

        Contraction();

        Vector3 lightVector = new Vector3(lightSpeed, 0, 0); // Initialize a light Vector.
        Vector3 currentVel = velocity;

        // If the space bar is pressed down by the player do the following:
        if (Input.GetKeyDown("up"))
        {
            velocity = (velocity + lightVector*0.9999999f)*0.5f; // Update the speed with the speed of light
        }
        if (Input.GetKeyDown("down"))
        {
            velocity = (velocity - new Vector3 (10f,0f,0f));
        }
        if (Input.GetKeyDown("left"))
        {
            velocity = (velocity + new Vector3(0f,0f,10f));

            if (velocity.z < -lightSpeed)
                velocity.z = lightSpeed * 0.9999f;
        }
        if (Input.GetKeyDown("right"))
        {
            velocity = (velocity - new Vector3(0f, 0f, 10f));

            if (velocity.z > lightSpeed)
                velocity.z = lightSpeed * 0.9999f;
        }

        refFrameEngine.setVelocity(currentVel); // Set the Velocity in the Physics Engine in the MainEngine.
        // Update the speed counter display
        speedCount = currentVel.magnitude / lightSpeed;
        SetSpeedText();

        //currentVel.magnitude = Mathf.Clamp(velocity.magnitude, -lightSpeed, lightSpeed);


    }
    // Initialize some size limitations for our object.
    // Yes, Vince, I know this is not good for general coding.
    private Vector3 minSize = Vector3.zero;
    private Vector3 normSize = new Vector3(10f, 10f, 10f);

    // This function will be our length contraction
    void Contraction()
    {

        // Determine the current speed and calculate the gamma factor.
        // Coundn't think of a better way to do this, so I broke everything into its component forms and then
        // changed the scale factors based on the velocities in the different component directions.
        float currentXSpeed = velocity.x;
        float currentYSpeed = velocity.y;
        float currentZSpeed = velocity.z;

        float OneOverGammaX = Mathf.Sqrt(1 - Mathf.Pow(currentXSpeed / lightSpeed, 2f));
        float OneOverGammaY = Mathf.Sqrt(1 - Mathf.Pow(currentYSpeed / lightSpeed, 2f));
        float OneOverGammaZ = Mathf.Sqrt(1 - Mathf.Pow(currentZSpeed / lightSpeed, 2f));

        // Change the object size of the objects in our bodies list based on the gamma factor
        foreach (GameObject contractable in bodies)
        {   // Transform the scale of the object based on the speed of the incoming object.
            contractable.transform.localScale = new Vector3(normSize.x * OneOverGammaX,
                normSize.y * OneOverGammaY, normSize.z * OneOverGammaZ);

            // The code below just as an emergency incase the size ever gets negative.
            if (contractable.transform.localScale.x < minSize.x)
                contractable.transform.localScale = normSize;

            if (contractable.transform.localScale.y < minSize.y)
                contractable.transform.localScale = normSize;

            if (contractable.transform.localScale.z < minSize.z)
                contractable.transform.localScale = normSize;
        }

    }

    // Update the speed counter display that the player will view.
    void SetSpeedText()
    {   // Basically just turns everything into a string and writes it in unity.
        speedText.text = (velocity.magnitude/lightSpeed).ToString() + "c";
    }

}
