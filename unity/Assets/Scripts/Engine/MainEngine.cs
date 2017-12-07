using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEngine : MonoBehaviour {

	public Rigidbody rb;
	public Vector3 velocityVector; //average velocity this FixedUpdate()
	private Vector3 netForceVector; 
	public List<Vector3> forceVectorList = new List<Vector3>();

	void FixedUpdate ()
	{
		SumForces();
		UpdateState();
	}

	void SumForces()
	{
		netForceVector = Vector3.zero;

		// Sum over all of the forces to calculate the total force.
		foreach (Vector3 forceVector in forceVectorList)
		{
			netForceVector += forceVector;
		}
	}

	// The UpdateState method uses the semi-implicit Euler step.
	// This is the method used by most commericial game engines.
	// The semi-implicit Euler step is different than the explicit
	// Euler step by one simple change: you calculate the new velocity
	// FIRST, and THEN calculate the new position.
	void UpdateState()
	{
        // Define the accelerationVector as a Vector3.
        Vector3 accelerationVector;

        // Handle the case where there is no associated Rigidbody.
        if (rb != null)
        {
            accelerationVector = netForceVector / rb.mass;
        }
        else
        {
            accelerationVector = Vector3.zero;
        }

		// Update the velocity vector
		velocityVector += accelerationVector * Time.deltaTime;

		// Update the position vector AFTER the velocity vector.
		// This is a key piece of using the semi-implicit Euler.
		transform.position += velocityVector * Time.deltaTime;

	}
    // Set the velocity outside the MainEngine.
    public void setVelocity(Vector3 vel)
    {
        velocityVector = vel;
    }
}

