using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 5;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0) //If health reaches 0, destroy the enemy!
        {
            Animator anim = gameObject.GetComponentInParent<Animator>();
            anim.SetTrigger("Trigger");
            Destroy(transform.parent.gameObject, 1f);
        }
    }

    //Using OnTriggerEnter to check for the bullet
    // CHANGE THE BULLET TO USING ISTRIGGER IN COLLIDER SETTINGS //
    // YOU ALSO NEED TO TAG THE BULLET LIKE WE DID THE COLLECTIBLE! //
    // SPECIFICALLY, TAG THE CHILD OBJECT 'bullet' //
    void OnTriggerEnter(Collider other) 
    {
        //If we hit something tagged with bullet
        if (other.gameObject.CompareTag("bullet")) 
        {
            //Reduce the enemies health
            health -= 1;
            //Destroy the bullet
            Destroy(other.gameObject);
        }
    }
}