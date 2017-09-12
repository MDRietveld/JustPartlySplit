using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour {

	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt ("Highscore", 0);
	}
}
