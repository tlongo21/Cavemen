using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReferenceFrameEngine : MonoBehaviour {

    public static List<GameObject> bodies;    // This is the list of object to be contracted.
    private Vector3 velocity;  // This is the initial velocity of the object.
    private Vector3 position;
    private float refFrameSpeed;
    private float refFrameDirection;

    private float lightSpeed = 100f; // Speed of light

    public Text speedText; // A class for adding text
    private float speedCount; // Counter that will display the speed 

    private GameObject refFrame;       // Call the MainEngine
    private FreeCamera refFrameObj;

    private float V_x;
    private float V_z;

    // On start up Unity will run this code
    void Start()
    { 
        // On the start up, initialize the variables.
        refFrame = GameObject.Find("Main Camera");
        refFrameObj = GetComponent<FreeCamera>();

        // Use the velocity vector determined from the Physics Engine
        refFrameSpeed = refFrameObj.get_Speed();
        refFrameDirection = refFrameObj.get_direction_angle();
        
        // Initialize the counter.
        speedCount = 0.1f/lightSpeed;
        SetSpeedText(); // Call our UI update function.
    }

    private void OnEnable()
    {
        
    }
}

    private Vector3 minSize = Vector3.zero;
    private Vector3 normSize = new Vector3 (5f,5f,5f);

    // This is the update loop which works similarly to the FixedUpdate loop
    void Update()
    {       
        // Update the speed counter display
        speedCount = refFrameSpeed / lightSpeed;
        
        // Take the absolute movement speed and convert it into its components.
        V_x = refFrameSpeed * Mathf.Sin(refFrameDirection);
        V_z = refFrameSpeed * Mathf.Cos(refFrameDirection);
        // print(refFrameDirection);

        SetSpeedText();

        // On the start up, initialize the variables.
        refFrame = GameObject.Find("Main Camera");
        refFrameObj = GetComponent<FreeCamera>();

        // Use the velocity vector determined from the Physics Engine
        refFrameSpeed = refFrameObj.get_Speed();
        print(refFrameSpeed);
        refFrameDirection = refFrameObj.get_direction_angle();

        float V_ref_x = refFrameSpeed * Mathf.Sin(refFrameDirection);
        float V_ref_z = refFrameSpeed * Mathf.Cos(refFrameDirection);

        float currentXSpeed = Mathf.Abs(V_ref_x);
        float currentZSpeed = Mathf.Abs(V_ref_z);

        float scaleX_adj = Mathf.Sqrt(1 - Mathf.Pow(currentXSpeed / lightSpeed, 2f));
        float scaleZ_adj = Mathf.Sqrt(1 - Mathf.Pow(currentZSpeed / lightSpeed, 2f));
        this.transform.localScale = new Vector3(scaleX_adj * normSize.x, normSize.y, scaleZ_adj * normSize.z);     
    }
   
    // Update the speed counter display that the player will view.
    void SetSpeedText() {   // Basically just turns everything into a string and writes it in unity.
        speedText.text = "Velocity" + (speedCount).ToString() + "c";
    }

}
