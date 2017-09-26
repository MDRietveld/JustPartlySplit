using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class RepeatingBackground : MonoBehaviour 
{

	private BoxCollider2D groundCollider;
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
		if (gameObject.tag == "bgFast") {
			if (GameControl.instance.startBgAt == GameControl.instance.goingToBg) {
				GameControl.instance.getNewRandom ();
			}
			GameControl.instance.frontMap++;
			if (GameControl.instance.frontMap == 4) {
				GameControl.instance.frontMap = 1;
			}
		}

		if (gameObject.tag == "bgSlow") {
			GameControl.instance.backMap++;
			if (GameControl.instance.backMap == 3) {
				GameControl.instance.backMap = 1;
			}
		}

		if (gameObject.tag == "bgSlow" && GameControl.instance.frontMap == 1) {
			version = GameControl.instance.startBgAt + "." + GameControl.instance.goingToBg;
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Textures/bgcomplete/bgSlow" + version, typeof(Sprite)) as Sprite;
		} else if (gameObject.tag == "bgSlow" && GameControl.instance.frontMap == 2) {
			gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Textures/bgcomplete/bgSlow" + GameControl.instance.goingToBg, typeof(Sprite)) as Sprite;
        }

		if (gameObject.tag == "bgFast" && GameControl.instance.frontMap == 1) {
			gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Textures/bgcomplete/bgFast" + GameControl.instance.startBgAt, typeof(Sprite)) as Sprite;
		} else if (gameObject.tag == "bgFast" && GameControl.instance.frontMap == 2) {
			version = GameControl.instance.startBgAt + "." + GameControl.instance.goingToBg;
			gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Textures/bgcomplete/bgFast" + version, typeof(Sprite)) as Sprite;
		} else if (gameObject.tag == "bgFast" && GameControl.instance.frontMap == 3) {
			gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Textures/bgcomplete/bgFast" + GameControl.instance.goingToBg, typeof(Sprite)) as Sprite;
			GameControl.instance.getNewRandom ();
		}
        //This is how far to the right we will move our background object, in this case, twice its length. This will position it directly to the right of the currently visible background object.
        Vector3 groundOffSet = new Vector3(18f * 2f, 0, 0);

		//Move this object from it's position offscreen, behind the player, to the new position off-camera in front of the player.
		transform.position = (Vector3) transform.position + groundOffSet;
	}
}