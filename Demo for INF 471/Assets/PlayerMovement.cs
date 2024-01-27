using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 movement;
    private Rigidbody rb;

    //This variable is visible in the Inspector, because it's public!
    public int speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        //Pull the Rigidbody component from the GameObject on the first frame, and put it in a variable.
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //OnMove is called whenever the player uses WASD - thanks, InputSystem!
    void OnMove(InputValue moveVal)
    {
        //First, we need to convert computer numbers into a Vector2 (2D coordinates)
        movement = moveVal.Get<Vector2>();
        //Now we split those into an X and Y values
        float movex = movement.x;
        float movey = movement.y;
        //Then, we plug those 2D coordinates into 3D space (Vector3)
        Vector3 movement3 = new Vector3(movex, 0.0f, movey);
        //Now we add force to the Rigidbody according to that Vector3
        rb.AddForce(movement3 * speed); //We multiply the Vector3 by the speed variable to increase the force!
    }
}
