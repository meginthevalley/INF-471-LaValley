using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonPlayer : MonoBehaviour
{
    //input the camera into the script
    public GameObject cam;
    public float moveSpeed = 0.1f;
    public float jumpHeight; //OPTIONAL
    public GameObject bulletSpawner;
    public GameObject bullet;
    public float mouseSensitivity = 50;

    private float xRotation = 0;
    float mouseX;
    private CharacterController controller;
    Vector2 movement;
    
    //OPTIONAL: for Jump code!//
    private bool hasJumped = false;
    private float jSpeed;
    private bool sprinting = false;
    private float overallSprintSpeed = 2;

    // Start is called before the first frame update
    void Start()
    {
        //lock the cursor of the player to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;

        controller = GetComponent<CharacterController>();

        jumpHeight = 1;

        jSpeed = 0;
    }                                                                                                                                                                                                                                                                                                                                                                                                                                           

    // Update is called once per frame
    void Update()
    {
        //Whenever you want to apply a rotation you need to convert from Quaternion to Euler
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up *mouseX);

        //Vector3 for the direction we are moving in
        Vector3 moveDir = (transform.forward * movement.y) + (transform.right * movement.x);

        //OPTIONAL: code for jumping!//
        if(hasJumped) //If the player pressed 'space' this frame...
        {
            jSpeed = jumpHeight; //'jSpeed' is normally artificial gravity, but we set it to Jump height

            hasJumped = false; //Only do it once - set it to false!
        
        }

        jSpeed -= 9.8f * Time.deltaTime; //Artificial gravity

        moveDir.y = jSpeed; //We weren't using the Y of moveDir before - we'll use it for the jump!
       
        if(sprinting) //If the player pressed 'shift' this frame...
        {
            moveDir.x = moveDir.x * overallSprintSpeed; 
            moveDir.z = moveDir.z * overallSprintSpeed;
            if(Input.GetKeyUp(KeyCode.LeftShift))
            {
            sprinting = false;
            }
        }

        controller.Move(moveDir * moveSpeed);
    }

    // OPTIONAL - Jump code! YOU NEED TO SET THIS UP IN THE INPUT ASSET!! //
    void OnJump(InputValue jumpValue) 
    {
        if (controller.isGrounded) //This is a check that the controller gives us!
        {
            hasJumped = true;
        }
    }

    void OnFire(InputValue fireValue) 
    {
        //Instantiate the bullet
        Instantiate(bullet, bulletSpawner.transform.position, bulletSpawner.transform.rotation);
    }

    void OnLook(InputValue lookValue)
    {
        //Get a Vector2 from lookValue
        Vector2 mouseLook = lookValue.Get<Vector2>();

        //Separate X & Y
        mouseX = mouseLook.x * Time.deltaTime * mouseSensitivity;

        float mouseY = mouseLook.y *Time.deltaTime * mouseSensitivity;

        //Use the Y for up and down rotation of the camera
        xRotation -= mouseY;

        //clamp the Xrotation so the player can not look behind themselves
        xRotation = Mathf.Clamp(xRotation, -90, 90);
    }

    void OnMove(InputValue moveValue)
    {
        movement = moveValue.Get<Vector2>();
    }
    void OnSprint(InputValue jumpValue) 
    {
            sprinting = true;
    }
}
