using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCamera : MonoBehaviour
{

    public float moveSpeed; // This variable will be defined by the user. 
    private float rotateSpeed = 5f;

    void Update()
    {

        Vector3 position = transform.position; //Initialize the position and the rotation of the camera
        Vector3 angles = transform.eulerAngles;

        if (Input.GetKey("up")) // If the up arrow is pushed move forward 
            {
            position.z += moveSpeed * Time.deltaTime;
            }
        if (Input.GetKey("down")) // down arrow = backward
            {
            position.z -= moveSpeed * Time.deltaTime;
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
        // update the zoom, note that this was too slow originally so I made it 100 times faster for convenience.
        position.y += scroll * moveSpeed * 100f * Time.deltaTime;

        // if the mouse is clicked and dragged the camera rotation will change.
        if (Input.GetMouseButton(0))
        {
            angles.y += Input.GetAxis("Mouse X")*rotateSpeed; // I don't know why angles.y is for the X axis of the mouse pad.
            angles.x -= Input.GetAxis("Mouse Y")*rotateSpeed; // Same for angles.x.
        }
        // I also have no idea what the Quaternion does. But this script works so I'm not changing it.
        Quaternion rotation = Quaternion.Euler(angles.x, angles.y, 0);

        // Lastly, update the position and rotation on each update.
        transform.position = position;
        transform.rotation = rotation;

    }
}
