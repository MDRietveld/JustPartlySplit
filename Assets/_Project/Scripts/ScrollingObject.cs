﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour 
{
	private Rigidbody2D rb2d;
	[Header("test")]
	public float scrollSpeed = -1f;

	// Use this for initialization
	void Start () 
	{
		//Get and store a reference to the Rigidbody2D attached to this GameObject.
		rb2d = GetComponent<Rigidbody2D>();

		//Start the object moving.
		rb2d.velocity = new Vector2 (0, 0);
	}

	void Update()
	{
        if (GameControl.instance.startGame)
            rb2d.velocity = Vector2.zero;
        else
            rb2d.velocity = new Vector2(scrollSpeed, 0);
        // If the game is over, stop scrolling.
        if (GameControl.instance.gameOver == true)
			rb2d.velocity = Vector2.zero;
	}
}