using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField]
    private float speed = 1;
    [SerializeField]
    private float maxGravity = -1;
    [SerializeField]
    private float gravityRate = 0.1f;
    [SerializeField]
    private float jumpHeight = 1;

    [HideInInspector]
    public Vector2 move_Input;
    private Vector3 movement;
    [HideInInspector]
    public float currentY;
    [HideInInspector]
    public CharacterController controller;
    [HideInInspector]
    public bool jumpRequest;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentY = maxGravity;
    }

    // Update is called once per frame
    void OnMove(InputValue moveValue)
    {
        move_Input = moveValue.Get<Vector2>();
    }
    void OnJump(InputValue jumpValue)
    {
        jumpRequest = true;
    }
    public void ApplyJump()
    {
        currentY = jumpHeight;
        jumpRequest = false;
    }
    public void ApplyGravity()
    {
        //subtract the gravity rate
        currentY -= gravityRate;
        //Clamp!
        currentY = Mathf.Clamp(currentY, maxGravity, jumpHeight);
    }
    public void MovePlayer()
    {
        //Create the Vector3 movement vector
        movement = new Vector3(move_Input.x, currentY, move_Input.y);
        //Rotate the player
        if(move_Input != Vector2.zero)
        {
            Vector3 lookVector = new Vector3(movement.x, 0, movement.z);
            Quaternion toRotation = Quaternion.LookRotation(lookVector, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 10);
        }
        controller.Move(movement * speed * Time.deltaTime);
    }
}
