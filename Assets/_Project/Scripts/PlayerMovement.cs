using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour {

	public float upForce = 200f;
	private bool arrowToTheKnee = false;
	private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (arrowToTheKnee == true)
			return;
//		Debug.Log (transform.position.y);
		if (Input.GetMouseButtonDown (0)) {
			rb2d.velocity = Vector2.zero;
			rb2d.AddForce (new Vector2 (0, upForce));
//			anim.SetTrigger ("Flap");
		}
	}
	void OnCollisionEnter2D (Collision2D collided){
		if (collided.gameObject.tag == "Obstacle"){

			Debug.Log (collided.collider.bounds.center);
			// (-1.3, 0.8, -10.0)
			arrowToTheKnee = true;
			GameControl.instance.GameOver ();
		}
	}
}
