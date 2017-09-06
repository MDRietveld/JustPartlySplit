using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerMovement : MonoBehaviour {

    private AudioSource audio;
    public AudioClip hitSound;
    public AudioClip dieSound;
    public AudioClip jumpSound;

    public float upForce = 250f;
	private bool arrowToTheKnee = false;
	private Rigidbody2D rb2d;
	private Animator anim;
	public Text gameOverText;

	private bool _jump = true;

	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource> ();
        rb2d = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
        if (GameControl.instance.startGame)
            return;
		if (arrowToTheKnee == true) {
            anim.SetTrigger("Dead");
            return;
        }

		if (rb2d.velocity.y == 0f && _jump) {
			anim.SetTrigger ("Walk");
			_jump = false;
			GameControl.instance.JumpReady ();
		}
	}

	public void Jump(){
		anim.SetTrigger ("Idle");
		_jump = true;

		rb2d.velocity = Vector2.zero;
		rb2d.AddForce (new Vector2 (0, upForce));
        audio.PlayOneShot(jumpSound);

    }

	public void BlobbyDied(){
		anim.SetTrigger ("Dead");
        audio.PlayOneShot(dieSound);
        arrowToTheKnee = true;
	}

	void OnCollisionEnter2D (Collision2D collision){
//		Collider2D collider = collision.collider;
		if(collision.gameObject.tag == "Obstacle") {
			anim.SetTrigger ("Dead");
			gameOverText.text = "Ohh snap, he vanished";
			arrowToTheKnee = true;
			Destroy (gameObject);
			GameControl.instance.GameOver ();
		}
	}
}
