using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonPlayer : MonoBehaviour
{
    //input the camera into the script
    public GameObject cam;
    private float xRotation = 0;
    public float mouseSensitivity = 75;
    float mouseX;
    private CharacterController controller;
    Vector2 movement;
    public float moveSpeed = 1;
    // Start is called before the first frame update
    void Start()
    {
        //lock the cursor of the player to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
    }                                                                                                                                                                                                                                                                                                                                                                                                                                           

    // Update is called once per frame
    void Update()
    {
        //Whenever you want to apply a rotation you need to convert from Quaternion to Euler
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up *mouseX);
        //Vector3 for the direction we are moving in
        Vector3 moveDir = (transform.forward * movement.y) + (transform.right * movement.x);
        controller.Move(moveDir * moveSpeed);
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
}
