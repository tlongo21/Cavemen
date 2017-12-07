using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomBody : MonoBehaviour
{
	public static List<CustomBody> CustomBodies; // Define a List of CustomBodies
	public Rigidbody rb_self;  // Use the Rigidbody from Unity

	// Define a list of booleans to set whether certain bodies participate
	// in different forces, e.g. whether a body participates in mutual
	// gravitational attraction. Defaults to true.
	public bool isGravitational = false;
	public bool isSpringVertex  = false;
    private bool doesContract = false;
	public float springConstant = 0;

    private GameObject refFrame;       // Call the previous engine.
    private FreeCamera refFrameObj;
    private float refFrameSpeed;
    private float refFrameDirection;

    private float lightSpeed = 100f;
    public Vector3 normSize;

    // Define some physical constants.
    float G = 0.5f;							// Gravitational constant

	void FixedUpdate ()
	{
		gravitation();
		spring();
    }

    private void Update()
    {
        contract();
    }

    void OnEnable()
	{
		if (CustomBodies == null)
			CustomBodies = new List<CustomBody>();
		CustomBodies.Add(this);

        if (this.tag == "contractable")
        {
            doesContract = true;
        } 
	}

	private void OnDisable()
	{
		CustomBodies.Remove(this);
	}


    private void contract()
    {
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



	// Define a function to calculate the TOTAL gravitational force
	// on an object from every other object in the scene which was
	// labeled "isGravitational".
	void gravitation() {
		foreach (CustomBody body in CustomBodies)
		{
			// Access the RigidBody properties of the selected EXTERNAL body.
			Rigidbody rb_external = body.rb_self;

			// Instantiate some variables used in the if statement.
			Vector3 displacement_vector = Vector3.zero;
			float distance, forceMagnitude;

			// Iterate over all bodies in the program EXCEPT the one currently selected.
			if ((body != this) && (isGravitational == true))

				// Define a displacement vector that goes from the external object
				// to the object for which we are calculating the forces.
				displacement_vector = rb_external.position - rb_self.position;

				distance = displacement_vector.magnitude;
				
				// Handle the case of zero distance, which implies infinite gravitation. Here,
				// we can get away with just doing nothing. This will suffice for now.
				if (distance == 0f)
					return;

				forceMagnitude = G * (rb_self.mass * rb_external.mass) / Mathf.Pow(distance, 2);
				Vector3 force = displacement_vector.normalized * forceMagnitude;

				rb_self.AddForce(force);
			
		}
	}

	// Define a function to calculate the TOTAL gravitational force
	// on an object from every other object in the scene which was
	// labeled "isSpringVertex".
	void spring() {
		foreach (CustomBody body in CustomBodies)
		{ 
		
			// Access the RigidBody properties of the selected EXTERNAL body.
			Rigidbody rb_external = body.rb_self;

			// Instantiate some variables used in the if statement.
			Vector3 displacement_vector = Vector3.zero;
			float distance, forceMagnitude;

			// Iterate over all bodies in the program EXCEPT the one currently selected.
			if ( (body != this) && ((body.isSpringVertex == true)) )

				// Define a displacement vector that goes from the external object
				// to the object for which we are calculating the forces.
				displacement_vector = rb_self.position - rb_external.position;

				distance = displacement_vector.magnitude;
				
				// Handle the case of zero distance, which implies infinite gravitation. Here,
				// we can get away with just doing nothing. This will suffice for now.
				if (distance == 0f)
					return;

				forceMagnitude = body.springConstant * distance;
				Vector3 force = -displacement_vector.normalized * forceMagnitude;

				rb_self.AddForce(force);
			
		}
	}

}