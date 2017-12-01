using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCamera : MonoBehaviour
{

    public float moveSpeed; // This variable will be defined by the user. 
    public Vector3 velocity; // Store the camera's (player's) velocity
    private float rotateSpeed = 5f;

    void Update()
    {
        //Initialize the position and the rotation of the camera
        Vector3 position = transform.position; 
        Vector3 angles = transform.eulerAngles;

        // If the up arrow is pushed move forward
        if (Input.GetKey("up"))  
            {
            position.z += moveSpeed * Time.deltaTime;
            }

        if (Input.GetKey("right")) // right arrow = right
            {
            position.x += moveSpeed * Time.deltaTime;
            }

        if (Input.GetKey("left")) // left arrow = left
            {
            position.x -= moveSpeed * Time.deltaTime;
            }

        // detect the scroll wheel from unity and use this to zoom in and out.
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        // update the zoom, note that this was too slow originally so
        // I made it 100 times faster for convenience.
        position.y += scroll * moveSpeed * 100f * Time.deltaTime;

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
