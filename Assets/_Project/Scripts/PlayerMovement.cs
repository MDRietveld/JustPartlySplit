using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour {

	public float upForce = 250f;
	private bool arrowToTheKnee = false;
	private Rigidbody2D rb2d;

//	private bool jumpReady = true;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (arrowToTheKnee == true)
			return;
	}

	void OnTriggerEnter2D(Collider2D other){
		GameControl.instance.JumpReady ();
	}

	public void Jump(){
		rb2d.velocity = Vector2.zero;
		rb2d.AddForce (new Vector2 (0, upForce));
	}

	void OnCollisionEnter2D (Collision2D collision){
		Collider2D collider = collision.collider;
		float RectWidth = GetComponent<Collider2D> ().bounds.size.x;
		float RectHeight = GetComponent<Collider2D> ().bounds.size.y;

		if(collision.gameObject.tag == "Obstacle")
		{
//			Debug.Log ("Collision Obstacle");
//			bool collideFromLeft = false;
//			bool collideFromTop = false;
//			bool collideFromRight = false;
//			bool collideFromBottom = false;
//			float circleRad = collider.bounds.size.x;

			Vector3 contactPoint = collision.contacts[0].point;
			Vector3 center = collider.bounds.center;
//			Debug.Log (contactPoint);
//			Debug.Log (center);

			if (contactPoint.y > center.y && //checks that circle is on top of rectangle
				(contactPoint.x < center.x + RectWidth / 2 && contactPoint.x > center.x - RectWidth / 2)) {
//				collideFromTop = true;
				Debug.Log ("TOP");
			}
			if (contactPoint.y < center.y &&
				(contactPoint.x < center.x + RectWidth / 2 && contactPoint.x > center.x - RectWidth / 2)) {
//				collideFromBottom = true;
				Debug.Log ("BOTTOM");
			}
			if (contactPoint.x > center.x &&
				(contactPoint.y < center.y + RectHeight / 2 && contactPoint.y > center.y - RectHeight / 2)) {
//				collideFromRight = true;
				Debug.Log ("RIGHT");
			}
			Debug.Log (collision.contacts[0].point);
			Debug.Log ("Contact y: " + contactPoint.y);
			Debug.Log ("Center y: " + center.y);
			if (contactPoint.x < center.x &&
				(contactPoint.y < center.y + RectHeight / 2 && contactPoint.y > center.y - RectHeight / 2)) {
//				collideFromLeft = true;
				Debug.Log ("LEFT");
			}

//			Debug.Log (collideFromLeft);
//			Debug.Log (collideFromTop);
//			Debug.Log (collideFromRight);
//			Debug.Log (collideFromBottom);

			arrowToTheKnee = true;
			GameControl.instance.GameOver ();
		}
//		if (collided.gameObject.tag == "Obstacle"){
//
//			Vector3 center = collided.collider.bounds.center;
//			Vector3 contactPoint = collided.contacts[0].point;
//			// (-1.3, 0.8, -10.0)
//
//			bool right = contactPoint.x > center.x;
//			bool top = contactPoint.y > center.y;
//			if (right) Debug.Log("Right");
//			if (top) Debug.Log("Top");
//			Debug.Log (right);
//			Debug.Log (top);

//		}
	}
}
