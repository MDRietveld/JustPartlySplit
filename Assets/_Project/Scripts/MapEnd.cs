using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEnd : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "StartView") {
            GameControl.instance.LoadMap();
        }else if (collision.tag == "EndView") {
            Destroy(transform.parent.gameObject);
        }
    }
}
