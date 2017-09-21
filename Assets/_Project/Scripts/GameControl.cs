using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour 
{
	public static GameControl instance;         

	public GameObject pauseButton;
	public GameObject gameOverText;
	public GameObject gameOverText2;
	public GameObject highscoreText;
	public GameObject previousRunText;
	public Text scoreText;
    public Text startText;


	private AudioSource audio;
	public AudioClip dieSound;
	public AudioClip landSound;

    private int score = 0;            
	public bool gameOver = false;     
	public float scrollSpeed = -1f;

	public bool jumpReady = false;
//    private bool _jumpReady = false;
    public bool startGame = true;
	private bool _first = true;

	PlayerMovement[] players;

	public int frontUpMap = 0;
	public int backUpMap = 0;
	public int frontDownMap = 0;
	public int backDownMap = 0;

	private ScrollingObject[] scrollingObjects;

	[Header("DISABLE this to test maps")]
	public bool autoLoad = true;

    

    void Awake()
	{
		if (instance == null){
			audio = GetComponent<AudioSource> ();

			if (PlayerPrefs.HasKey ("Highscore") && PlayerPrefs.HasKey("PreviousRun") && PlayerPrefs.HasKey("VolumeSetting")) {
				AudioListener.volume = PlayerPrefs.GetInt("VolumeSetting");
                highscoreText.GetComponentInChildren<Text>().text = "Highscore \n" + PlayerPrefs.GetInt("Highscore");
				previousRunText.GetComponentInChildren<Text>().text = "Last Run \n" + PlayerPrefs.GetInt("PreviousRun");
            } else {
				PlayerPrefs.SetInt ("Highscore", 0);
				PlayerPrefs.SetInt ("PreviousRun", 0);
				highscoreText.GetComponentInChildren<Text>().text = "Highscore \n0";
				previousRunText.GetComponentInChildren<Text>().text = "Last Run \n0";
			}

            instance = this;
		}else if(instance != this)
			Destroy (gameObject);
	}

	void OnMouseDown(){
		if (startGame){
			startGame = false;
			startText.enabled = false;
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
		} else if (startGame) {
			return;
		}
		if (gameOver) {
			//...reload the current scene.
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape)) { Application.Quit(); }

		if(startText.enabled == true)
			startText.color = new Color(startText.color.r, startText.color.g, startText.color.b, (Mathf.Sin(Time.time * 2.0f) + 1.0f)/2.0f);
		if (jumpReady && Input.GetMouseButtonDown(0)) {
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

			RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
			if (hit.collider != null) {
				Debug.Log (hit.collider.gameObject.tag);
				if (hit.collider.gameObject.tag != "Volume" && hit.collider.gameObject.tag != "Pause") {
					players = gameObject.GetComponentsInChildren<PlayerMovement>();
					if (!_first) {
						foreach (PlayerMovement player in players) {
							player.Jump ();
						}
					} else {
						_first = false;
					}
				}
			}
		}
	}

	public void setJumpReady(){
		if (!jumpReady) {
			Debug.Log ("play sound?");
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
		PlayerPrefs.SetInt ("PreviousRun", score);
		jumpReady = false;
		pauseButton.SetActive (false);
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