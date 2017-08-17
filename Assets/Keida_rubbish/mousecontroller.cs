using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mousecontroller : MonoBehaviour {
    public float jetPackForce = 75.0f;
    public Rigidbody2D rb;
    public float forwardMovementSpeed = 3.0f;
	void Start () {
        rb=GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        bool jetpackActive = Input.GetButton("Fire1");
        if (jetpackActive)
        {
            rb.AddForce(new Vector2(0, jetPackForce));
        }

        Vector2 newVelocity = rb.velocity;
        newVelocity.x = forwardMovementSpeed;
        rb.velocity = newVelocity;
	}
}
