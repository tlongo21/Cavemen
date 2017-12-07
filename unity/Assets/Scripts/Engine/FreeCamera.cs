using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCamera : MonoBehaviour
{

	public float moveSpeed=99f; // This variable will be defined by the user. 
    public Vector3 positionVector;
    private Vector3 velocityVector; // Store the camera's (player's) velocity
	private float rotateSpeed = 2.5f;

	void FixedUpdate()
	{
		//Initialize the position and the rotation of the camera
		positionVector = transform.position; 
		Vector3 angles = transform.eulerAngles;

		// Convert theta to radians. angles.y represents the direction
		// in the x-z plane (left-handed), which is the horizontal, flat direction
		// in Unity.
		float theta = Mathf.PI / 180 * angles.y;

		// Take the absolute movement speed and convert it into its components.
		float V_x = moveSpeed * Mathf.Sin(theta);
		float V_z = moveSpeed * Mathf.Cos(theta);

		// Define the same quantities but rotated to use horizontal movement.
		float V_xh = moveSpeed * Mathf.Sin(theta + Mathf.PI/2);
		float V_zh = moveSpeed * Mathf.Cos(theta + Mathf.PI/2);

		// If the up arrow is pushed move forward
		if (Input.GetKey("w") || Input.GetKey("up"))  
		{
			positionVector.z += V_z * Time.deltaTime;
			positionVector.x += V_x * Time.deltaTime;
		}

		if (Input.GetKey("s") || Input.GetKey("down"))
		{
			positionVector.z -= V_z * Time.deltaTime;
			positionVector.x -= V_x * Time.deltaTime;
		}

		if (Input.GetKey("a") || Input.GetKey("left"))
		{
			// We switch 
			positionVector.z -= V_zh * Time.deltaTime;
			positionVector.x -= V_xh * Time.deltaTime;
		}

		if (Input.GetKey("d") || Input.GetKey("right"))
		{
			positionVector.z += V_zh * Time.deltaTime;
			positionVector.x += V_xh * Time.deltaTime;
		}

		// detect the scroll wheel from unity and use this to zoom in and out.
		float scroll = Input.GetAxis("Mouse ScrollWheel");

		// update the zoom, note that this was too slow originally so
		// I made it 100 times faster for convenience.
		//position.y += scroll * moveSpeed * 100f * Time.deltaTime;

		// if the mouse is clicked and dragged the camera rotation will change.
		if (Input.GetMouseButton(0))
		{
			// I don't know why angles.y is for the X axis of the mouse pad.
			angles.y += Input.GetAxis("Mouse X")*rotateSpeed; 

			// Same for angles.x.
			angles.x -= Input.GetAxis("Mouse Y")*rotateSpeed; 
		}
		// I also have no idea what the Quaternion does. 
		// But this script works so I'm not changing it. 
		Quaternion rotation = Quaternion.Euler(angles.x, angles.y, 0);

		// Lastly, update the position and rotation on each update.
		transform.position = positionVector;
		transform.rotation = rotation;
	}

    public void setPosition(Vector3 pos)
    {
        positionVector = pos;
    }

    public float get_Speed()
    {
        return moveSpeed;
    }

    public float get_direction_angle()
    {
        return transform.eulerAngles.y;
    }

}
