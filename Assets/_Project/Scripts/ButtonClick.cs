using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ButtonClick : MonoBehaviour {

	void Start(){
		if (gameObject.tag == "Volume" && AudioListener.volume == 0) {
			GetComponent<Button> ().image.overrideSprite = Resources.Load ("Textures/SoundOff", typeof(Sprite)) as Sprite;
		}
	}

	void OnMouseDown() {

		if (gameObject.tag == "Pause") {
			if (Time.timeScale == 1) {
				GetComponent<Button> ().image.overrideSprite = Resources.Load ("Textures/PlayButton", typeof(Sprite)) as Sprite;
				Time.timeScale = 0;
			} else {
				GetComponent<Button> ().image.overrideSprite = Resources.Load ("Textures/PauzeButton", typeof(Sprite)) as Sprite;
				Time.timeScale = 1;
			}
		} else if (gameObject.tag == "Volume"){
			if (AudioListener.volume == 1) {
				GetComponent<Button> ().image.overrideSprite = Resources.Load ("Textures/SoundOff", typeof(Sprite)) as Sprite;
				AudioListener.volume = 0;
			} else {
				GetComponent<Button> ().image.overrideSprite = Resources.Load ("Textures/SoundOn", typeof(Sprite)) as Sprite;
				AudioListener.volume = 1;
			}
		}

	}
}
