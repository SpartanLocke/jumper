﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour
{
	[HideInInspector]
	public bool facingRight = true;			// For determining which way the player is currently facing.
	[HideInInspector]
	public bool jump = false;				// Condition for whether the player should jump.


	public float moveForce = 365f;			// Amount of force added to move the player left and right.
	public float maxXSpeed = 5f;				// The fastest the player can travel in the x axis.
    public float maxYSpeed = 5f;
	public float maxRotationSpeed = 750f;
    public float jumpForce = 1000f;			// Amount of force added when the player jumps.
    


	private Transform groundCheck;			// A position marking where to check if the player is grounded.
	private Transform sideCheck1;			// A position marking where to check if the player is grounded.
	private Transform sideCheck2;			// A position marking where to check if the player is grounded.
	private bool grounded = false;			// Whether or not the player is grounded.
	private bool onSide = false;
	public static float maxJumps = 2;
	private float jumps = maxJumps;
    
	void Awake()
	{
		// Setting up references.
		groundCheck = transform.Find("groundCheck");
		sideCheck1 = transform.Find("sideCheck1");
		sideCheck2 = transform.Find("sideCheck2");
    }


	void Update()
	{
		// The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));  
		onSide = Physics2D.Linecast(transform.position, sideCheck2.position, 1 << LayerMask.NameToLayer("Ground")) || Physics2D.Linecast(transform.position, sideCheck1.position, 1 << LayerMask.NameToLayer("Ground"));  
		if (grounded && jumps>0) {
			jumps = 0;
		}
		// If the jump button is pressed and the player is grounded then the player should jump.
		if (Input.GetKeyDown (KeyCode.UpArrow) && jumps < maxJumps) {
			if (!onSide) {
				jump = true;
			}
		}
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Scene");
        }
    }


	void FixedUpdate ()
	{
		// Cache the horizontal input.
		float h = Input.GetAxis("Horizontal");

		// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
		if(h * GetComponent<Rigidbody2D>().velocity.x < maxXSpeed)
			// ... add a force to the player.
			GetComponent<Rigidbody2D>().AddForce(Vector2.right * h * moveForce);

		// If the input is moving the player right and the player is facing left...
		if(h > 0 && !facingRight)
			// ... flip the player.
			Flip();
		// Otherwise if the input is moving the player left and the player is facing right...
		else if(h < 0 && facingRight)
			// ... flip the player.
			Flip();

		// If the player should jump...
		if(jump)
		{

			// Add a vertical force to the player.
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));

			// Make sure the player can't jump again until the jump conditions from Update are satisfied.
			jump = false;
			jumps++;
		}

        // If the player's horizontal velocity is greater than the maxSpeed...
        if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxXSpeed)
        {
            // ... set the player's velocity to the maxSpeed in the x axis.
            GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxXSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }
        if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y) > maxYSpeed)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, Mathf.Sign(GetComponent<Rigidbody2D>().velocity.y) * maxYSpeed);
        }
		GetComponent<Rigidbody2D>().angularVelocity = Mathf.Clamp(GetComponent<Rigidbody2D>().angularVelocity, -maxRotationSpeed, maxRotationSpeed);
    }


    void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

}
