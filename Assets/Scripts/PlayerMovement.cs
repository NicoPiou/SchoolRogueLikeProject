using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public bool isWarrior;
	public bool isMage;
	public bool isThief;

	public Transform warriorPosition;
	public Transform thiefPosition;
	public GameObject shield;
	private bool isShielding;

	private Animator animator;

	public string inputHorizontal;
	public string inputJump;
	public string playerName;

	public bool onGround;

	private Rigidbody2D rbPlayer;
	private float fadeSpeed;
	public float speed;
	private float actualSpeed;
	public float jumpForce;
	private int jumpCounter = 2;

	private bool doubleWalljumpCounter;
	[Range (0.0f, 1.0f)] public float angleAttaque = 0.5f;
	public float wallJumpReduction;

	// Use this for initialization
	void Start () {
		rbPlayer = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
		actualSpeed = speed;
	}

	// Update is called once per frame
	void Update () 
	{
		GroundChecking ();


		if (isThief) 
		{
			DoubleJumping ();
		} 
		else 
		{
			Jumping ();
		}

		if (isMage)
		{
			Swapping ();
		}

		if (isWarrior) 
		{
			Shielding ();
		}

	}

	void FixedUpdate()
	{
		if (!isShielding) 
		{
			Moving ();
		}
	}

	void GroundChecking()
	{
		Vector2 physicsCentre = new Vector2 (this.transform.position.x + this.GetComponent<BoxCollider2D> ().offset.x, this.transform.position.y + this.GetComponent<BoxCollider2D> ().offset.y);

		//test si le joueur était onGround la frame avant
		bool wasOnGround;
		if (onGround) 
		{
			wasOnGround = true;
		} 
		else 
		{
			wasOnGround = false;
		}

		float j = -1f;
		for (int i = 0; i < 7; i++) 
		{
			Vector2 rayStartPoint = physicsCentre + new Vector2 (j * 0.49f, 0f);
			Debug.DrawRay (rayStartPoint, Vector2.down*0.6f, Color.red, 0.25f);
			int layerMask = ~(LayerMask.GetMask(playerName));
			RaycastHit2D hit = Physics2D.Raycast (rayStartPoint, Vector2.down, 0.6f, layerMask);
			if ( hit.collider != null && hit.collider.tag != playerName) 
			{
				onGround = true;
				break;
			}
			else 
			{
				onGround = false;
			}

			j += 0.33f;
		}


		if (!wasOnGround && onGround) 
		{
			jumpCounter = 2;
			//sound Atterrissage
		}
	}

	void Moving ()
	{
		float straffe = 0f; 

		straffe = Input.GetAxis (inputHorizontal);
		if (straffe > 0) {
			transform.localScale = new Vector2 (1.0f, 1.0f);
			actualSpeed = speed;
		}
		if (straffe < 0) {
			transform.localScale = new Vector2 (-1.0f, 1.0f);
			actualSpeed = -speed;
		}
		straffe *= actualSpeed;
		straffe *= Time.deltaTime;


		if (straffe == 0) 
		{
			animator.SetBool("Walking", false);
		} 
		else 
		{
			animator.SetBool("Walking", true);
		}



		Vector2 force = new Vector2 (straffe, 0.0f);
		force = transform.localToWorldMatrix.MultiplyVector (force);
		rbPlayer.AddForce (force, ForceMode2D.Force);

		Vector2 v = rbPlayer.velocity;
		v.x = 0f;
		rbPlayer.velocity = v;
	}

	void Jumping ()
	{
		if (Input.GetKeyDown(inputJump) && onGround) 
		{
			rbPlayer.velocity = new Vector2 (rbPlayer.velocity.x, jumpForce);
		}
	}

	void DoubleJumping ()
	{
		if (Input.GetKeyDown(inputJump) && jumpCounter > 0) 
		{
			rbPlayer.velocity = new Vector2 (rbPlayer.velocity.x, jumpForce);
			jumpCounter--;
		}
	}

	void Swapping () 
	{
		Debug.Log ("can swap");
		Vector3 myPos = transform.position;

		if (Input.GetKeyDown("joystick 2 button 1")) 
		{
			Debug.Log ("swap Warrior");
			transform.position = new Vector3 (warriorPosition.position.x, warriorPosition.position.y, warriorPosition.position.z);
			warriorPosition.position = new Vector3 (myPos.x, myPos.y, myPos.z);
		}

		if (Input.GetKeyDown("joystick 2 button 3")) 
		{
			Debug.Log ("swap Thief");
			transform.position = new Vector3 (thiefPosition.position.x, thiefPosition.position.y, thiefPosition.position.z);
			thiefPosition.position = new Vector3 (myPos.x, myPos.y, myPos.z);
		}
	}

	void Shielding ()
	{
		if (Input.GetKey ("joystick 1 button 1")) 
		{
			shield.SetActive(true);
			isShielding = true;
			animator.SetBool ("Shielding", true);
		} 
		else 
		{
			shield.SetActive(false);
			isShielding = false;
			animator.SetBool ("Shielding", false);
		}
	}
}
