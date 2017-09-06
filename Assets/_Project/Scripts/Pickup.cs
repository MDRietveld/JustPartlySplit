using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Pickup : MonoBehaviour {
    private AudioSource audio;
    private Renderer render;

    private bool triggered = false;

    // Use this for initialization
    void Start () {
        audio = gameObject.GetComponent<AudioSource>();
        render = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update () {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !triggered)
        {
            triggered = true;
            render.enabled = false;
            audio.Play();
            Destroy(gameObject, 5);     //Delay object destruction, ohterwise sound will not be played
            //Todo: add one to score
        }
    }
}
