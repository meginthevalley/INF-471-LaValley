using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 1;
    public float lifetime = 1;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        //Get the Rigidbody on the bullet
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Add force every frame that the bullet exists
        rb.AddForce(transform.forward * speed);
        //Destroy the object after it exists for 'lifetime' seconds
        Destroy(gameObject, lifetime);
    }
}
