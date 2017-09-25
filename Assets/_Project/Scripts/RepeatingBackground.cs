using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class RepeatingBackground : MonoBehaviour 
{

	private BoxCollider2D groundCollider;
	private int repeatedBackMap = 0;
	private int repeatedFrontMap = 0;
	private int currentBack = 1;
	private int currentFront = 1;
	private int goingToBack = 1;
	private int goingToFront = 1;
	private int current = 1;
	private int goingTo = 1;
	private string version;

	//Update runs once per frame
	private void Update()
	{
        //Check if the difference along the x axis between the main Camera and the position of the object this is attached to is greater than groundHorizontalLength.
        if (transform.position.x < -18f)
		{
			//If true, this means this object is no longer visible and we can safely move it forward to be re-used.
			RepositionBackground ();
		}
	}

	//Moves the object this script is attached to right in order to create our looping background effect.
	private void RepositionBackground()
	{
        // Count for back background
        if (gameObject.tag == "BBGUp") {
            GameControl.instance.backUpMap++;
			if (GameControl.instance.backUpMap == 3) {
				GameControl.instance.backUpMap = 1;
				currentBack = goingToBack;
//				current = goingTo;
			}
            repeatedBackMap = GameControl.instance.backUpMap;
        }
        // Count for front background
        if (gameObject.tag == "FBGUp") {
            GameControl.instance.frontUpMap++;
			if (GameControl.instance.frontUpMap == 4) {
				GameControl.instance.frontUpMap = 1;
				currentFront = goingToFront;
//				current = goingTo;
			}
            repeatedFrontMap = GameControl.instance.frontUpMap;
        }
        if (gameObject.tag == "BBGDown") {
            GameControl.instance.backDownMap++;
			if (GameControl.instance.backDownMap == 3) {
				GameControl.instance.backDownMap = 1;
				currentBack = goingToBack;
//				current = goingTo;
			}
            repeatedBackMap = GameControl.instance.backDownMap;
        }
        // Count for front background
        if (gameObject.tag == "FBGDown") {
            GameControl.instance.frontDownMap++;
			if (GameControl.instance.frontDownMap == 4) {
				GameControl.instance.frontDownMap = 1;
				currentFront = goingToFront;
//				current = goingTo;
			}
            repeatedFrontMap = GameControl.instance.frontDownMap;
        }
//


		if (gameObject.tag != "Floor") {
			if (currentFront == goingToFront) {
				if (goingToFront == GameControl.instance.newRandomFast) {
					GameControl.instance.getNewRandom();
					goingToFront = GameControl.instance.newRandomFast;
				} else {
					goingToFront = GameControl.instance.newRandomFast;
				}
			}
//
			if (currentBack == goingToBack) {
				if (goingToBack == GameControl.instance.newRandom) {
					GameControl.instance.getNewRandom();
					goingToBack = GameControl.instance.newRandom;
				} else {
					goingToBack = GameControl.instance.newRandom;
				}
			}
//			if(current == goingTo){
//				if (goingTo == GameControl.instance.newRandom) {
//					GameControl.instance.getNewRandom();
//					goingTo = GameControl.instance.newRandom;
//				} else {
//					goingTo = GameControl.instance.newRandom;
//				}
//			}
//			version = current + "." + goingTo;
//			Debug.Log ("Version " + version);
//			Debug.Log ("current " + current);
//			Debug.Log ("goingTo " + goingTo);
//			Debug.Log ("Version " + version);
//			Debug.Log ("currentFront " + currentBack);
//			Debug.Log ("goingToFront " + goingToBack);
		}




        if (gameObject.tag == "BBGDown" && repeatedBackMap == 1) {
			version = currentBack + "." + goingToBack;
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Textures/Background/BackDown" + version, typeof(Sprite)) as Sprite;
        } else if (gameObject.tag == "BBGUp" && repeatedBackMap == 1) {
			version = currentBack + "." + goingToBack;
			gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Textures/Background/BackUp" + version, typeof(Sprite)) as Sprite;
        } else if (gameObject.tag == "BBGDown" && repeatedBackMap == 2) {
			
			gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Textures/Background/BackDown" + goingToBack, typeof(Sprite)) as Sprite;
//			gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Textures/Background/BackDown" + goingTo, typeof(Sprite)) as Sprite;
        } else if (gameObject.tag == "BBGUp" && repeatedBackMap == 2) {
//			Debug.Log ("currentFront " + currentBack);
//			Debug.Log ("goingToFront " + goingToBack);
			gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Textures/Background/BackUp" + goingToBack, typeof(Sprite)) as Sprite;
//			gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Textures/Background/BackUp" + goingTo, typeof(Sprite)) as Sprite;
		}

        if (gameObject.tag == "FBGDown" && repeatedFrontMap == 1) {
			Debug.Log ("Front 1");
			Debug.Log ("currentFront " + currentFront);
			Debug.Log ("goingToFront " + goingToFront);
			gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Textures/Background/FrontDown" + currentFront, typeof(Sprite)) as Sprite;
//			gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Textures/Background/FrontDown" + current, typeof(Sprite)) as Sprite;
		} else if (gameObject.tag == "FBGUp" && repeatedFrontMap == 1) {
			gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Textures/Background/FrontUp" + currentFront, typeof(Sprite)) as Sprite;
//			gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Textures/Background/FrontUp" + current, typeof(Sprite)) as Sprite;
		} else if (gameObject.tag == "FBGDown" && repeatedFrontMap == 2) {
			Debug.Log ("Front 2");
			Debug.Log ("currentFront " + currentFront);
			Debug.Log ("goingToFront " + goingToFront);
			version = currentFront + "." + goingToFront;
			gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Textures/Background/FrontDown" + version, typeof(Sprite)) as Sprite;
        } else if (gameObject.tag == "FBGUp" && repeatedFrontMap == 2) {
			version = currentFront + "." + goingToFront;
			gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Textures/Background/FrontUp" + version, typeof(Sprite)) as Sprite;
		} else if (gameObject.tag == "FBGDown" && repeatedFrontMap == 3) {
			Debug.Log ("Front 3");
			Debug.Log ("currentFront " + currentFront);
			Debug.Log ("goingToFront " + goingToFront);
			gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Textures/Background/FrontDown" + goingToFront, typeof(Sprite)) as Sprite;
//			gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Textures/Background/FrontDown" + goingTo, typeof(Sprite)) as Sprite;
		} else if (gameObject.tag == "FBGUp" && repeatedFrontMap == 3) {
			gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Textures/Background/FrontUp" + goingToFront, typeof(Sprite)) as Sprite;
//			gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Textures/Background/FrontUp" + goingTo, typeof(Sprite)) as Sprite;
        }
        //This is how far to the right we will move our background object, in this case, twice its length. This will position it directly to the right of the currently visible background object.
        Vector3 groundOffSet = new Vector3(18f * 2f, 0, 0);

		//Move this object from it's position offscreen, behind the player, to the new position off-camera in front of the player.
		transform.position = (Vector3) transform.position + groundOffSet;
	}
}