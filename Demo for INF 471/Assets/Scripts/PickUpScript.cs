using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickUpScript : MonoBehaviour
{
    //Game Object for the player (needs to be dragged in the script from the inspector)
    public GameObject player;
    //create an integer for score
    public int score;
    private TextMeshProUGUI textComponent;
    public GameObject scoreText;
    public GameObject endText;

    // Start is called before the first frame update
    void Start()
    {
        textComponent = scoreText.GetComponent<TextMeshProUGUI>();
        //set score equal to 0
        score = 0;
        //changes the TExtMeshUGUI text for the game object score text
        textComponent.text = "Score:" + score.ToString();
        
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
    //The on trigger enter function allows you to tell when another game object has collided with the one that the script is on
    //The other game object must have "is Trigger" checked in the collider section in order to read it though
    void OnTriggerEnter(Collider other){
        //registers if the other game object has the tag PickUp
        if( other.gameObject.CompareTag("PickUp")){
            //deletes the pickup from scene
            other.gameObject.SetActive(false);
            //add one to score whenever a collectible is picked up
            score += 1;
            //print the score to console
            print(score.ToString());
            //changes the TExtMeshUGUI text for the game object score text
            textComponent.text = "Score:" + score.ToString();
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
    }
}
