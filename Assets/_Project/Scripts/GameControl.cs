using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour 
{
	public static GameControl instance;         
	public Text scoreText;                      
	public GameObject gameOverText;
	public GameObject gameOverText2;
    public Text startText;


	private AudioSource audio;
	public AudioClip dieSound;

    private int score = 0;            
	public bool gameOver = false;     
	public float scrollSpeed = -1f;

	private bool _jumpReady = true;
    public bool startGame = true;

	PlayerMovement[] players;

	void Awake()
	{
		if (instance == null){
			audio = GetComponent<AudioSource> ();
			instance = this;
		}else if(instance != this)
			Destroy (gameObject);
	}

	void Update()
	{
        if (startGame && Input.GetMouseButtonDown(0)){
            startGame = false;
            startText.enabled = false;
			InvokeRepeating("scoreCount", 0, 1.0f);
			return;
        } else if (startGame) {
            return;
        }
        //If the game is over and the player has pressed some input...
        if (gameOver && Input.GetMouseButtonUp(0)) {
			//...reload the current scene.
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}

		if (Input.GetMouseButtonDown (0) && _jumpReady) {
			_jumpReady = false;
			players = gameObject.GetComponentsInChildren<PlayerMovement>();
			foreach (PlayerMovement player in players) {
				player.Jump ();
			}
		}

//		score++;
//		scoreText.text = "Score: " + score.ToString();
	}

	public void JumpReady(){
		_jumpReady = true;
	}


	public void scoreCount()
	{
		if (gameOver)   
			return;
		//If the game is not over, increase the score...
		score += 50;
		//...and adjust the score text.
		scoreText.text = "Score: " + score.ToString();
	}

	public void coinPickup(){
		if (gameOver)   
			return;
		//If the game is not over, increase the score...
		score += 10;
		//...and adjust the score text.
		scoreText.text = "Score: " + score.ToString();
	}

	public void GameOver()
	{
		_jumpReady = false;

		gameOverText.SetActive (true);
		gameOverText2.SetActive (true);
		startText.enabled = true;
		startText.text = "Tap to reset";
		players = gameObject.GetComponentsInChildren<PlayerMovement>();
		foreach (PlayerMovement player in players) {
			player.BlobbyDied ();
		}
		audio.PlayOneShot(dieSound);
		gameOver = true;
	}
}