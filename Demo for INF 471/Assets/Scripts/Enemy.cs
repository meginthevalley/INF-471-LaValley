using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //creates a vector 3 to rotate the enemy
    public Vector3 rotateChangeEnemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //uses the update function to continuously rotate the enemy
        transform.Rotate(rotateChangeEnemy);
    
        
    }
}
