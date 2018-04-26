using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FireTrap : MonoBehaviour {

	public float fireRate = 0.5f;
	private float nextFire = 0f;
	public int ejectSpeed = 100;

	public AudioClip shotSound;
	AudioSource audioSource;

	public GameObject other;
	public Rigidbody bulletCasing;
	private Rigidbody bullet;

	// Use this for initialization
	void Start () {

		audioSource = GetComponent<AudioSource>();
		bullet = other.GetComponent<Rigidbody>();





		
	}
	
	// Update is called once per frame
	void Update () {

		if (Time.time > nextFire) {

			nextFire = Time.time + fireRate;

			bullet = Instantiate (bulletCasing, transform.position, transform.rotation);

			audioSource.PlayOneShot (shotSound, 0.7F);
			bullet.velocity = transform.TransformDirection (Vector3.left * ejectSpeed);
		}
	}
}
