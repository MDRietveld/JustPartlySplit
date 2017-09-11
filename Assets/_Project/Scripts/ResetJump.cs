using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetJump : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D (Collision2D collided){
		
		if (collided.gameObject.tag == "Player" && collided.gameObject.GetComponent<Rigidbody2D>().velocity.y == 0) {
			Debug.Log (collided.gameObject.tag);
		}
	}
}
