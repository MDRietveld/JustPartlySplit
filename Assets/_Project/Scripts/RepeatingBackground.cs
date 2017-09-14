using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class RepeatingBackground : MonoBehaviour 
{

	private BoxCollider2D groundCollider;       //This stores a reference to the collider attached to the Ground.
//	private float groundHorizontalLength;       //A float to store the x-axis length of the collider2D attached to the Ground GameObject.
	private int repeatedBackMap = 0;
	private int repeatedFrontMap = 0;
	//Awake is called before Start.
	private void Awake ()
	{
		//Get and store a reference to the collider2D attached to Ground.

//		groundCollider = GetComponent<BoxCollider2D> ();
		//Store the size of the collider along the x axis (its length in units).
//		groundHorizontalLength = groundCollider.size.x;
	}

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
        if (gameObject.tag == "BBGUp")
        {
            GameControl.instance.backUpMap++;
            repeatedBackMap = GameControl.instance.backUpMap;
        }
        // Count for front background
        if (gameObject.tag == "FBGUp")
        {
            GameControl.instance.frontUpMap++;
            repeatedFrontMap = GameControl.instance.frontUpMap;
        }
        if (gameObject.tag == "BBGDown")
        {
            GameControl.instance.backDownMap++;
            repeatedBackMap = GameControl.instance.backDownMap;
        }
        // Count for front background
        if (gameObject.tag == "FBGDown")
        {
            GameControl.instance.frontDownMap++;
            repeatedFrontMap = GameControl.instance.frontDownMap;
        }

        if (gameObject.tag == "BBGDown" && repeatedBackMap == 1)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Textures/Background/BackDown1.5", typeof(Sprite)) as Sprite;
        }
        else if (gameObject.tag == "BBGUp" && repeatedBackMap == 1)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Textures/Background/BackUp1.5", typeof(Sprite)) as Sprite;
        }
        else if (gameObject.tag == "BBGDown" && (repeatedBackMap == 2 || repeatedBackMap == 3))
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Textures/Background/BackDown2", typeof(Sprite)) as Sprite;
        }
        else if (gameObject.tag == "BBGUp" && (repeatedBackMap == 2 || repeatedBackMap == 3))
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Textures/Background/BackUp2", typeof(Sprite)) as Sprite;
        }


        if (gameObject.tag == "FBGDown" && repeatedFrontMap == 3)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Textures/Background/FrontDown1.5", typeof(Sprite)) as Sprite;
        }
        else if (gameObject.tag == "FBGUp" && repeatedFrontMap == 3)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Textures/Background/FrontUp1.5", typeof(Sprite)) as Sprite;
        }
        else if (gameObject.tag == "FBGDown" && (repeatedFrontMap == 4 || repeatedFrontMap == 5))
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Textures/Background/FrontDown2", typeof(Sprite)) as Sprite;
        }
        else if (gameObject.tag == "FBGUp" && (repeatedFrontMap == 4 || repeatedFrontMap == 5))
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Textures/Background/FrontUp2", typeof(Sprite)) as Sprite;
        }
        //This is how far to the right we will move our background object, in this case, twice its length. This will position it directly to the right of the currently visible background object.
        Vector3 groundOffSet = new Vector3(18f * 2f, 0, 0);

		//Move this object from it's position offscreen, behind the player, to the new position off-camera in front of the player.
		transform.position = (Vector3) transform.position + groundOffSet;
	}
}