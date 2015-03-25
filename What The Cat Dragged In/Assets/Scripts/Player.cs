using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
	//////////VARIABLES///////////
	#region
	
	// Utility //
	float timer = 0f;
	
	// Player Stats //
	public int currentHealth = 10;
	public int maxHealth = 10;
	
	// Speed Settings //
	public Vector3 speed;
	public Vector3 sprintSpeed;
	public Vector3 jumpSpeed;
	public Vector3 gravity;
	
	// Sprinting Variables //
	public float sprintTime = 1.0f;
	public bool sprinting = false;
	float sprintingTimer;
	
	// Jumping Variables //
	public float jumpHeight = 2.0f;
	public float maxJumpHeight = 2.0f;
	float maxJumpSpeed;
	public bool grounded = true;
	public bool reachedApex;
	float jumpVelocity = 0.0f;
	Vector3 jumpVector;
	public float jumpCounter;
	
	public List<GameObject> Children;
	
	
	#endregion
	/////////////////////////////
	
	//////////FUNCTIONS///////////
	
	// Initialization //
	void Start ()
	{

	}
	
	// Update //
	void Update ()
	{
		timer += Time.deltaTime;
	

		InputManager();
	}

	void InputManager()
	{
		Movement();
		Sprint();
		Jump();
	}

	// Movement //
	void Movement()
	{
		//if(Input.GetButtonDown("Horizontal"))
		//{
			if(Input.GetAxis("Horizontal") < 0)
			{
				transform.position -= speed * Time.deltaTime;
			}
		else if(Input.GetAxis("Horizontal") > 0)
				transform.position += speed * Time.deltaTime;
		//}
	}


	// Sprint //
	void Sprint()
	{
		if (Input.GetButtonDown("Sprint") &&
		    sprinting == false &&
		    grounded == true)
		{
			sprinting = true;
			sprintingTimer = 0.0f;
			Debug.Log("Sprinting!", gameObject);
		}
		
		if(sprinting == true)
		{
			sprintingTimer += Time.deltaTime;
		}
		
		if (sprintingTimer > sprintTime && grounded == true)
		{
			sprinting = false;
		}
	}
	
	// Jump //
	void Jump()
	{
		//Check if grounded
		if(Input.GetButtonDown("Jump") && grounded == true)
		{
			grounded = false;
			jumpVelocity = 0.0f;
		}
		
		//Check if apex is reached and not on the ground, if true continue to jump
		if (grounded == false && reachedApex == false)
		{
			float newPosition = Mathf.SmoothDamp(
				transform.position.y,
				jumpHeight, 
				ref jumpVelocity, 
				1.0f * Time.deltaTime, 
				jumpSpeed.y);
			
			transform.position = new Vector3(transform.position.x, newPosition, transform.position.z);
		}
		
		//Check if jump height is reached
		if (transform.position.y >= maxJumpHeight)
		{
			reachedApex = true;
			jumpVelocity = 0.0f;
		}
		
		//Start acting gravity on the player to bring it back to the ground
		if (reachedApex == true)
		{
			float newPosition = Mathf.SmoothDamp(
				transform.position.y,
				0.0f,
				ref jumpVelocity,
				1.0f * Time.deltaTime, 
				gravity.y);
			
			transform.position = new Vector3(transform.position.x, newPosition, transform.position.z);

		}
		
		//Player land
		if (grounded == false && transform.position.y <= 0.0f)
		{
			transform.position = new Vector3(transform.position.x, 0.0f, transform.position.z);
			grounded = true;
			reachedApex = false;
		}        
	}
	
	
	bool GetSprinting()
	{
		return sprinting;
	}

	void AdjustCurrentHealth(int health)
	{
		currentHealth += health;
	}
}