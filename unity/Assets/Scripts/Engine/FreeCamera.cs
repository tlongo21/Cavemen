using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCamera : MonoBehaviour
{

	public float moveSpeed = 100; // This variable will be defined by the user. 
	public Vector3 velocity; // Store the camera's (player's) velocity
	private float rotateSpeed = 5f;

	void Update()
	{
		//Initialize the position and the rotation of the camera
		Vector3 position = transform.position; 
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
			position.z += V_z * Time.deltaTime;
			position.x += V_x * Time.deltaTime;
		}

		if (Input.GetKey("s") || Input.GetKey("down"))
		{
			position.z -= V_z * Time.deltaTime;
			position.x -= V_x * Time.deltaTime;
		}

		if (Input.GetKey("a") || Input.GetKey("left"))
		{
			// We switch 
			position.z -= V_zh * Time.deltaTime;
			position.x -= V_xh * Time.deltaTime;
		}

		if (Input.GetKey("d") || Input.GetKey("right"))
		{
			position.z += V_zh * Time.deltaTime;
			position.x += V_xh * Time.deltaTime;
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
		transform.position = position;
		transform.rotation = rotation;

	}
}
