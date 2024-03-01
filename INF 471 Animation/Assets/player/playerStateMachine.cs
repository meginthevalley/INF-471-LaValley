using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class playerStateMachine : MonoBehaviour
{
    [SerializeField]
    private Animator playerAnim;
    [SerializeField]
    private float deadZone = 0.1f;

    private PlayerMovement playerMove;

    private enum State
    {
        Idle,
        Run,
        Jump,
        Fall,
    }
    private State currentState;

    // Start is called before the first frame update
    void Start()
    {
     playerMove.GetComponent<PlayerMovement>();
        SwitchState(State.Idle);
    }

    // Update is called once per frame
    void Update()
    {
        //Apply Gravity in Update
     playerMove.ApplyGravity();

        //What State are we in?
        print(currentState);
        switch(currentState)
        {
            case State.Idle:
            DoIdle();
            break;

            case State.Run:
            DoRun();
            break;

            case State.Jump:
            DoJump();
            break;

            case State.Fall:
            DoFall();
            break;
        }  
    }
    private void SwitchState(State newState)
    {
        //Animation Triggers Go Here
        if(newState==State.Idle)
        {
            playerAnim.ResetTrigger("Run");
            playerAnim.SetTrigger("Idle");
        }
        else if(newState==State.Run)
        {
            playerAnim.ResetTrigger("Idle");
            playerAnim.SetTrigger("Run");
        }
        else if(newState==State.Jump)
        {
            playerAnim.SetTrigger("Jump");
         playerMove.ApplyJump();
        }
        else if(newState==State.Fall)
        {
            playerAnim.SetTrigger("Fall");
        }
        //Apply the new state
        currentState = newState;
    }
    private void DoIdle()
    {
        //updates
     playerMove.MovePlayer();

        //ExitConditions
        if  (playerMove.jumpRequest && playerMove.controller.isGrounded) //Jump
        {
            SwitchState(State.Jump);
        }
        else if ( playerMove.controller.isGrounded) //Fall
        {
            SwitchState(State.Fall);
        }
        else if  (playerMove.move_Input.magnitude >= deadZone) //Run
        {
            SwitchState(State.Run);
        }
    }
    private void DoRun()
    {
        //updates
     playerMove.MovePlayer();

        //ExitConditions
        if ( playerMove.jumpRequest && playerMove.controller.isGrounded) //Jump
        {
            SwitchState(State.Jump);
        }
        else if ( playerMove.controller.isGrounded) //Fall
        {
            SwitchState(State.Fall);
        }
        else if  (playerMove.move_Input.magnitude < deadZone) //Idle
        {
            SwitchState(State.Idle);
        }
    }
    private void DoJump()
    {
        //updates
     playerMove.MovePlayer();

        //Exit Conditions
        if (playerMove.currentY <=0) //Fall
        {
            SwitchState(State.Fall);
        }
    }
    private void DoFall()
    {
        //updates
     playerMove.MovePlayer();

         //ExitConditions
        if  (playerMove.controller.isGrounded) //We've Landed
        {
            if  (playerMove.move_Input.magnitude >= deadZone) //Run
            {
                SwitchState(State.Run);
            }
            else if  (playerMove.move_Input.magnitude < deadZone) //Idle
            {
                SwitchState(State.Idle);
            }
        }
    }
}
