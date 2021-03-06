﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour 
{
	public static GameControl instance;         

	public GameObject pauseButton;
	public GameObject volumeButton;
	public GameObject gameOverText;
	public GameObject gameOverText2;
	public GameObject highscoreText;
	public GameObject previousRunText;
	public GameObject startPanel;
	public GameObject statsPanel;
	public GameObject restartPanel;
	public GameObject backPanel;
	public GameObject allStatisticsPanel;
	public Text scoreText;
    public Text startText;
	public Text allStatisticsNumbersText;


	private AudioSource audio;
	public AudioClip dieSound;
	public AudioClip landSound;

    private int score = 0;            
	public bool gameOver = false;     
	public float scrollSpeed = -1f;

	public bool jumpReady = false;
//    private bool _jumpReady = false;
    public bool startGame = true;
//	private bool _first = true;

	PlayerMovement[] players;


	// Variables for repeating background
	public int frontMap = 0;
	public int backMap = 0;
	public int startBgAt = 1;
	public int goingToBg = 1;
	public int[] differentMaps = new int[]{1,2,3};

	private ScrollingObject[] scrollingObjects;

	[Header("DISABLE this to test maps")]
	public bool autoLoad = true;

    

    void Awake()
	{
		if (instance == null){
			audio = GetComponent<AudioSource> ();

			if (!PlayerPrefs.HasKey ("TotalJumps") && !PlayerPrefs.HasKey ("TotalScore") && !PlayerPrefs.HasKey ("AvarageScore") && !PlayerPrefs.HasKey ("TotalMaps") && !PlayerPrefs.HasKey ("TotalDeaths") && !PlayerPrefs.HasKey ("TotalCoins") && !PlayerPrefs.HasKey ("CurrentCoins")) {
				PlayerPrefs.SetInt ("TotalJumps", 0);
				PlayerPrefs.SetInt ("TotalScore", 0);
				PlayerPrefs.SetInt ("AvarageScore", 0);
				PlayerPrefs.SetInt ("TotalMaps", 0);
				PlayerPrefs.SetInt ("TotalDeaths", 0);
				PlayerPrefs.SetInt ("TotalCoins", 0);
				PlayerPrefs.SetInt ("CurrentCoins", 0);
			}

			if (PlayerPrefs.HasKey ("Highscore") && PlayerPrefs.HasKey("PreviousRun") && PlayerPrefs.HasKey("VolumeSetting")) {
				AudioListener.volume = PlayerPrefs.GetInt("VolumeSetting");
				highscoreText.GetComponentInChildren<Text>().text = "Highscore \n" + (string.Format("{0:0,0}", PlayerPrefs.GetInt("Highscore"))).Replace(",", ".");
				previousRunText.GetComponentInChildren<Text>().text = "Last Run \n" + (string.Format("{0:0,0}", PlayerPrefs.GetInt("PreviousRun"))).Replace(",", ".");
            } else {
				PlayerPrefs.SetInt ("Highscore", 0);
				PlayerPrefs.SetInt ("PreviousRun", 0);
				PlayerPrefs.SetInt ("VolumeSetting", 1);
				highscoreText.GetComponentInChildren<Text>().text = "Highscore \n0";
				previousRunText.GetComponentInChildren<Text>().text = "Last Run \n0";
			}

            instance = this;
		}else if(instance != this)
			Destroy (gameObject);
	}

	void Update()
	{
		if(PlayerPrefs.GetInt ("RestartGame") == 1 && startGame){
			PlayerPrefs.SetInt ("RestartGame", 0);
			startGame = false;
			startPanel.SetActive(false);
			statsPanel.SetActive(false);
			highscoreText.SetActive(false);
			previousRunText.SetActive(false);
			scoreText.enabled = true;
			scrollingObjects = GetComponentsInChildren<ScrollingObject> ();
			pauseButton.SetActive (true);
			jumpReady = true;
			if (autoLoad)
				CoursesLoader.instance.FirstCourse ();
			for (int i = 0; i < scrollingObjects.Length; i++) {
				scrollingObjects [i].enabled = true;
			}

			InvokeRepeating("scoreCount", 1.5f, 1.5f);
			return;
		}

		if (Input.GetKeyDown(KeyCode.Escape)) { Application.Quit(); }
		if (Input.GetMouseButtonDown (0)) {
			Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			Vector2 mousePos2D = new Vector2 (mousePos.x, mousePos.y);

			RaycastHit2D hit = Physics2D.Raycast (mousePos2D, Vector2.zero);
			Debug.Log (hit.collider.gameObject.tag);
			if (hit.collider != null) {
				if (hit.collider.gameObject.tag != "Volume" && hit.collider.gameObject.tag != "Pause" && jumpReady) {
					players = gameObject.GetComponentsInChildren<PlayerMovement> ();
					PlayerPrefs.SetInt ("TotalJumps", (PlayerPrefs.GetInt("TotalJumps") + 1));
					foreach (PlayerMovement player in players) {
						player.Jump ();
					}
				}else if(hit.collider.gameObject.tag == "StartGame" && startGame){
					startGame = false;
					startPanel.SetActive(false);
					statsPanel.SetActive(false);
					highscoreText.SetActive(false);
					previousRunText.SetActive(false);
					scoreText.enabled = true;
					scrollingObjects = GetComponentsInChildren<ScrollingObject> ();
					pauseButton.SetActive (true);
					jumpReady = true;
					if (autoLoad)
						CoursesLoader.instance.FirstCourse ();
					for (int i = 0; i < scrollingObjects.Length; i++) {
						scrollingObjects [i].enabled = true;
					}

					InvokeRepeating("scoreCount", 1.5f, 1.5f);
					return;
				}else if(hit.collider.gameObject.tag == "ShowStats"){
					
					allStatisticsNumbersText.text = "\n"+
						(string.Format("{0:0,0}", PlayerPrefs.GetInt("TotalJumps"))).Replace(",", ".")	+"\n"+
						(string.Format("{0:0,0}", PlayerPrefs.GetInt("TotalDeaths"))).Replace(",", ".")	+"\n"+
						(string.Format("{0:0,0}", PlayerPrefs.GetInt("TotalScore"))).Replace(",", ".")	+"\n"+
						(string.Format("{0:0,0}", PlayerPrefs.GetInt("AvarageScore"))).Replace(",", ".")	+"\n";
					startPanel.SetActive(false);
					statsPanel.SetActive(false);
					volumeButton.SetActive(false);
					highscoreText.SetActive(false);
					previousRunText.SetActive(false);
					allStatisticsPanel.SetActive(true);
				}else if(hit.collider.gameObject.tag == "CloseStats"){
					startPanel.SetActive(true);
					statsPanel.SetActive(true);
					volumeButton.SetActive(true);
					highscoreText.SetActive(true);
					previousRunText.SetActive(true);
					allStatisticsPanel.SetActive(false);
				}else if(hit.collider.gameObject.tag == "MainMenu"){
					if (gameOver) {
						//...reload the current scene.
						SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
					}
				}else if(hit.collider.gameObject.tag == "RestartGame"){
					PlayerPrefs.SetInt ("RestartGame", 1);
					if (gameOver) {
						//...reload the current scene.
						SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
					}
				}

			}

		}
//		if(startText.enabled == true)
//			startText.color = new Color(startText.color.r, startText.color.g, startText.color.b, (Mathf.Sin(Time.time * 2.0f) + 1.0f)/2.0f);
	}

	public void getNewRandom(){
		startBgAt = goingToBg;
		while(startBgAt == goingToBg){
			goingToBg = Random.Range (1, (differentMaps.Length + 1));
		}
	}

	public void setJumpReady(){
		if (!jumpReady) {
			audio.PlayOneShot(landSound);
			jumpReady = true;
		}
	}

	public void scoreCount()
	{
		if (gameOver)   
			return;
		score += 50;
		scoreText.text = "Score: " + score.ToString();
	}

	public void coinPickup(){
		if (gameOver)   
			return;
		score += 10;
		scoreText.text = "Score: " + score.ToString();
	}

	public void GameOver()
	{
		if (score > PlayerPrefs.GetInt("Highscore")){
			PlayerPrefs.SetInt ("Highscore", score);
		}
		PlayerPrefs.SetInt ("TotalDeaths", (PlayerPrefs.GetInt("TotalDeaths") + 1));
		PlayerPrefs.SetInt ("PreviousRun", score);
		PlayerPrefs.SetInt ("TotalScore", (PlayerPrefs.GetInt("TotalScore") + score));
		PlayerPrefs.SetInt ("AvarageScore", (PlayerPrefs.GetInt("TotalScore") / PlayerPrefs.GetInt("TotalDeaths")));
		jumpReady = false;
		pauseButton.SetActive (false);
		gameOverText.SetActive (true);
		gameOverText2.SetActive (true);
		restartPanel.SetActive (true);
		backPanel.SetActive (true);
//		startText.enabled = true;
//		startText.text = "Tap to reset";
		players = gameObject.GetComponentsInChildren<PlayerMovement>();
		foreach (PlayerMovement player in players) {
			player.BlobbyDied ();
		}
		audio.PlayOneShot(dieSound);
		gameOver = true;
	}
}