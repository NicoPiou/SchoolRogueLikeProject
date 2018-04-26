using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour {

	public Rigidbody rb;

	public bool Chevalier = false;
	public bool Voleur = false;
	public bool Mage = false;

	// Use this for initialization
	void Start () {

		rb.isKinematic = true;

		rb = GetComponent<Rigidbody> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter (Collider other){


		if (Chevalier == true) {
			if (other.tag == "Chevalier") {

				rb.isKinematic = false;

			}
		}
		if (Voleur == true) {
			if (other.tag == "Voleur") {

				rb.isKinematic = false;


			}
		}
		if (Mage == true) {
			if (other.tag == "Mage") {

				rb.isKinematic = false;


			}
		}
	}

	void OnCollisionExit(){

		rb.isKinematic = true;

	}
}
