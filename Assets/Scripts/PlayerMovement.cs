using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	public string inputHorizontal;
	public string inputJump;
	public string playerName;

	public bool onGround;

	private Rigidbody2D rbPlayer;
	private float fadeSpeed;
	public float speed;
	public float groundSpeed;
	public float jumpForce;
	private Vector2 previousPosition;
	public float airBufferDivider = 1f;

	// Use this for initialization
	void Start () {
		rbPlayer = GetComponent<Rigidbody2D> ();
		previousPosition = transform.position;
		foreach (string joystick in Input.GetJoystickNames()) {
			Debug.Log (joystick);
		}
	}

	// Update is called once per frame
	void Update () 
	{
		GroundChecking ();

		Jumping ();
	}

	void FixedUpdate()
	{
		Moving ();
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
			//sound Atterrissage
		}
	}

	void Moving ()
	{
		float translation = 0f;
		float straffe = 0f;

		/*if (onGround) 
		{*/
			straffe = Input.GetAxis (inputHorizontal) * (((speed * fadeSpeed)*groundSpeed));
			translation *= Time.deltaTime;
			straffe *= Time.deltaTime;
			//transform.Translate(straffe, 0.0f, translation);

			Vector2 force = new Vector3 (straffe, 0.0f, translation);
			force = transform.localToWorldMatrix.MultiplyVector (force);
			rbPlayer.AddForce (force, ForceMode2D.Force);

			Vector2 v = rbPlayer.velocity;
			v.x = 0f;
			rbPlayer.velocity = v;

			if (fadeSpeed < 1.1f)
			{
				fadeSpeed += 0.1f;
			}

			if (translation == 0 && straffe == 0 )
			{
				fadeSpeed = 0.5f;
			}
		/*}
		else 
		{
			straffe = Input.GetAxis (inputHorizontal) * speed;
			straffe *= Time.deltaTime;
			float airControlBuffer = CalculateAirControlBuffer (translation, straffe);
			Vector2 force = new Vector2 (straffe * airControlBuffer, 0.0f);
			force = transform.localToWorldMatrix.MultiplyVector (force);
			rbPlayer.AddForce (force, ForceMode2D.Force);
		}*/

		previousPosition = transform.position;
	}

	void Jumping ()
	{
		if (Input.GetKeyDown(inputJump) && onGround) 
		{
			rbPlayer.velocity = new Vector2 (rbPlayer.velocity.x, jumpForce);
		}
	}

	float CalculateAirControlBuffer (float translation, float straffe)
	{
		Vector2 currentDirection = new Vector2 (transform.position.x - previousPosition.x, transform.position.y - previousPosition.y);
		currentDirection = new Vector2 (currentDirection.x, 0f);
		Vector2 direction = new Vector2 (straffe, 0f);
		direction = transform.TransformDirection (direction);
		float angle = Vector2.Angle (currentDirection, direction);
		if (angle > 30) {
			float airControlBuffer = 1 + (angle / airBufferDivider);
			//Debug.Log (angle);
			return airControlBuffer;
		}
		return 1.0f;
	}
}
