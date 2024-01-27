using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour
{
    //Game Object for the player (needs to be dragged in the script from the inspector)
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //The on trigger enter function allows you to tell when another game object has collided with the one that the script is on
    //The other game object must have "is Trigger" checked in the collider section in order to read it though
    void OnTriggerEnter(Collider other){
        //registers if the other game object has the tag PickUp
        if( other.gameObject.CompareTag("PickUp")){
            //deletes the pickup from scene
            other.gameObject.SetActive(false);
        }
        //registers if the other game object has the tag PickUp
        if(other.gameObject.CompareTag("Enemy")){
            //deletes the player from the scene
            player.SetActive(false);
        }
    }
}
