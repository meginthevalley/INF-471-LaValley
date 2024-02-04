using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 movement;
    private Rigidbody rb;
    //Game Object for the player (needs to be dragged in the script from the inspector)
    public GameObject player;
    //create an integer for score
    public int score;
    private TextMeshProUGUI textComponent;
    public GameObject scoreText;
    public GameObject endText;
    bool gameRunning;

    //This variable is visible in the Inspector, because it's public!
    public int speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        //Pull the Rigidbody component from the GameObject on the first frame, and put it in a variable.
        rb = GetComponent<Rigidbody>();
        textComponent = scoreText.GetComponent<TextMeshProUGUI>();
        //set score equal to 0
        score = 0;
        //changes the TExtMeshUGUI text for the game object score text
        textComponent.text = "Score:" + score.ToString();
        gameRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
         if(score >= 5)
        {
            //calls the EndGame function
            EndGame();
        }  
    }

    //OnMove is called whenever the player uses WASD - thanks, InputSystem!
    void OnMove(InputValue moveVal)
    {
        if(gameRunning)
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
    }}
    //The on trigger enter function allows you to tell when another game object has collided with the one that the script is on
    //The other game object must have "is Trigger" checked in the collider section in order to read it though
    void OnTriggerEnter(Collider other){
        //registers if the other game object has the tag PickUp
        if( other.gameObject.CompareTag("PickUp")){
            //to get the animator component
            Animator anim = other.gameObject.GetComponentInParent<Animator>();
            //set the trigger to the animation trigger we made for the game object  
            anim.SetTrigger("collectibleTrigger");          
            //deletes the pickup from scene
            other.gameObject.SetActive(false);
            //add one to score whenever a collectible is picked up
            score += 1;
            //print the score to console
            print(score.ToString());
            //changes the TExtMeshUGUI text for the game object score text
            textComponent.text = "Score:" + score.ToString();
        }
        if( other.gameObject.CompareTag("PickUp2")){
            //to get the animator component
            Animator anim2 = other.gameObject.GetComponentInParent<Animator>();
            //set the trigger to the animation trigger we made for the game object  
            anim2.SetTrigger("Trigger3"); 
            other.gameObject.SetActive(false);
        }
        //registers if the other game object has the tag PickUp
        if(other.gameObject.CompareTag("Enemy")){
            //deletes the player from the scene
            player.SetActive(false);
        }
    }
    //function to show end game text
    void EndGame()
    {
        //set end game text to active
       endText.SetActive(true); 
       gameRunning = false;
    }
}
