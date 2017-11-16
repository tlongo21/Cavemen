using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test1 : MonoBehaviour {

    public Rigidbody rb;
    public Vector3 velocityVector; //average velocity this FixedUpdata()
    public Vector3 netForceVector; 
    public List<Vector3> forceVectorList = new List<Vector3>();

	void FixedUpdate ()
    {
        SumForces();
        UpdateVelocity();

        transform.position += velocityVector * Time.deltaTime;

	}
    void SumForces()
    {
        netForceVector = Vector3.zero;
        foreach (Vector3 forceVector in forceVectorList)
        {
            netForceVector = netForceVector + forceVector;
        }
    }
    void UpdateVelocity()
    {
        Vector3 accelerationVector = netForceVector / rb.mass;
        velocityVector += accelerationVector * Time.deltaTime;
    }
}
