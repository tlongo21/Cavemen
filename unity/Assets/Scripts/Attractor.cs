using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{

    public static List<Attractor> Attractors; //Define a List of Attractors
    const float G = 667.4f; //the Adjusted Gravitational Constant
    public Rigidbody rb;  // Use the Rigidbody from Unity

    // start the physics update loop. This loop updates all the physics parms every
    // time step interval. The length of the time set, is set in unity.
    void FixedUpdate ()
    {   // for each object in the Attractors list, call it in the function below.
        foreach (Attractor attractor in Attractors)
        {   // if the object is not itself, run the attraction function from below.

            if (attractor != this)
                Attract(attractor); // call the attractor function
        }
    }
    // when the scene begins do the following:
    void OnEnable()
    {
        if (Attractors == null) // if there are no objects in the list
            Attractors = new List<Attractor>(); // create a new list of attractors
        Attractors.Add(this); // and append all the objects in the scene into the list.
    }

    // when the scene ends do the following.
    private void OnDisable()
    {
        Attractors.Remove(this); // remove all objects from the list
    }

    // this is the main function that is basically the Gravitational Force
    void Attract(Attractor objToAttract)
    {   // first get the Rigidbody component of the object to be attracted
        Rigidbody rbToAttract = objToAttract.rb;

        // determine the distance between the object and the target
        Vector3 direction = rb.position - rbToAttract.position;
        float distance = direction.magnitude; // distance is the magnitude of the direction

        // if the distance is zero, break the loop
        // this was added to prevent errors involved in duplicating objects.
        if (distance == 0f)
            return;

        // Newtonian Force Law
        float forceMagnitude = G * (rb.mass * rbToAttract.mass) / Mathf.Pow(distance, 2);
        Vector3 force = direction.normalized * forceMagnitude;

        // Apply the force to object to be attracted every time step.
        rbToAttract.AddForce(force);
    }

}
