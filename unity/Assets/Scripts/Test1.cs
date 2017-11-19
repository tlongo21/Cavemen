using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test1 : MonoBehaviour {

    public Rigidbody rb;         // uses the Rigidbody feature from unity
    public Vector3 velocityVector; //average velocity this FixedUpdata()
    public Vector3 netForceVector;
    public List<Vector3> forceVectorList = new List<Vector3>();

  // physics update loop. Performs updates at each time step.
	void FixedUpdate ()
    {
        // apply the loops from below to what we are working on.
        SumForces();
        UpdateVelocity();

        // move the position based on the velocity
        transform.position += velocityVector * Time.deltaTime;

	}
    // add a force to the object
    void SumForces()
    {   // let the force vector be zero initially.
        netForceVector = Vector3.zero;
        // for each force vector in the vector list add them to the vector.
        foreach (Vector3 forceVector in forceVectorList)
        {
            netForceVector = netForceVector + forceVector;
        }
    }
    // change the velocity based on the forces acting on it and the objects mass
    void UpdateVelocity()
    {
        Vector3 accelerationVector = netForceVector / rb.mass;
        velocityVector += accelerationVector * Time.deltaTime;
    }
}
