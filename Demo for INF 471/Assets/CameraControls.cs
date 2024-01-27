using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public GameObject player; //drag player into box in the components to make sure this links
    private Vector3 offset; //creates a Vector3 called offset (for the camera offset)

    // Start is called before the first frame update
    void Start()
    {
        //gets the position of the camera related to the player
        offset = transform.position - player.transform.position;
    }

    // LateUpdate is good for cameras, bullets, etc.
    void LateUpdate()
    {
        //changes the position of the camera so that it follows the player using the offset
        transform.position = player.transform.position + offset;
    }
}

