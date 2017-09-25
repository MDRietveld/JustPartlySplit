using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour {
	public static CanvasController instance;


	private string[] rockText1 = new string[]{"Oooh pebbles", "I like rocks", "Danger!!!"};
	private string[] rockText2 = new string[]{"Rocks! Rocks! Rocks!", "Rocks can be tricky", "All around us..."};

	private string[] darkText1 = new string[]{"It's dark here", "Please.. light", "GET.. ME.. DOWN.."};
	private string[] darkText2 = new string[]{"Dark feelings...", "Lightbulb!", "Is it safe?"};

	private string[] spikesText1 = new string[]{"Looks dangerous!", "Painfully pointy!", "Danger!!!"};
	private string[] spikesText2 = new string[]{"Spikey thoughts!", "These thoughts.. why..", "All around us..."};

	private int _randNumber = 0;
	public bool speak = true;
	public string previous = "empty";

	// Use this for initialization
	void Awake () {
		if (instance == null) {
			instance = this;
		}  else if (instance != this)
			Destroy(gameObject);
	}

	public void mayISpeak(string trigger){
		if (speak && trigger != previous && (trigger == "Outside" || trigger == "Spikes" || trigger == "Rock")) {
			speak = false;
			previous = trigger;
			GameObject topText = (GameObject)Instantiate (Resources.Load<GameObject> ("Prefab/TopText"));
			GameObject bottomText = (GameObject)Instantiate (Resources.Load<GameObject> ("Prefab/BottomText"));

			topText.transform.SetParent (gameObject.transform, false);
			bottomText.transform.SetParent (gameObject.transform, false);

			if (trigger == "Outside") {
				_randNumber = Random.Range (0, darkText1.Length);
				topText.GetComponent<Text> ().text = darkText1 [_randNumber];
				bottomText.GetComponent<Text> ().text = darkText2 [_randNumber];
			}
			if (trigger == "Spikes") {
				_randNumber = Random.Range (0, spikesText1.Length);
				topText.GetComponent<Text> ().text = spikesText1 [_randNumber];
				bottomText.GetComponent<Text> ().text = spikesText2 [_randNumber];
			}
			if (trigger == "Rock") {
				_randNumber = Random.Range (0, rockText1.Length);
				topText.GetComponent<Text> ().text = rockText1 [_randNumber];
				bottomText.GetComponent<Text> ().text = rockText2 [_randNumber];
			}

			Destroy (topText, 3f);
			Destroy (bottomText, 3f);
			StartCoroutine(LetMeSpeak ());
		}
	}


	IEnumerator LetMeSpeak() {
		yield return new WaitForSeconds(4f);
		speak = true;
	}
}
