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
	public bool isGravitational = true;
	public bool isSpringVertex  = false;
	public float springConstant = 0;

	// Define some physical constants.
	float G = 0.5f;							// Gravitational constant

	void FixedUpdate ()
	{
		gravitation();
		spring();
	}

	void OnEnable()
	{
		if (CustomBodies == null)
			CustomBodies = new List<CustomBody>();
		CustomBodies.Add(this);
	}

	private void OnDisable()
	{
		CustomBodies.Remove(this);
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