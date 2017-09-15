using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour 
{
	public static GameControl instance;         
	                      
	public GameObject gameOverText;
	public GameObject gameOverText2;
	public GameObject highscoreText;
	public GameObject previousRunText;
	public Text scoreText;
    public Text startText;


	private AudioSource audio;
	public AudioClip dieSound;

    private int score = 0;            
	public bool gameOver = false;     
	public float scrollSpeed = -1f;

	public bool jumpReady = false;
    private bool _jumpReady = false;
    public bool startGame = true;

	PlayerMovement[] players;


//    public int mapPoolSize = 3;
	//private GameObject[] superEasyMaps;
 //   private GameObject[] easyMaps;
 //   private GameObject[] mediumMaps;
 //   private GameObject[] hardMaps;
 //   private GameObject firstObject;
	//private int superEasyI;
 //   private int easyI;
 //   private int mediumI;
 //   private int hardI;
 //   private int rand;
    //	public float spawnRate = 4f;

    //private GameObject[] maps;
    //private Vector2 objectPoolPosition = new Vector2(5f, 0);
    //	private float timeSinceLastSpawned;
    //	private float spawnXPosition = 10f;
    //private int currentMap = 0;
    //private int loadedMaps = 0;

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
   //         maps = new GameObject[2];
			//superEasyMaps = Resources.LoadAll<GameObject>("PrefabMaps/SuperEasy");
			//superEasyI = superEasyMaps.GetLength(0);
   //         easyMaps = Resources.LoadAll<GameObject>("PrefabMaps/Easy");
   //         easyI = easyMaps.GetLength(0);
   //         mediumMaps = Resources.LoadAll<GameObject>("PrefabMaps/Medium");
   //         mediumI = mediumMaps.GetLength(0);
   //         hardMaps = Resources.LoadAll<GameObject>("PrefabMaps/Hard");
   //         hardI = hardMaps.GetLength(0);
			//rand = Random.Range(0, superEasyI);


			if (PlayerPrefs.HasKey ("Highscore") && PlayerPrefs.HasKey("PreviousRun")) {
                highscoreText.GetComponentInChildren<Text>().text = "Highscore: " + PlayerPrefs.GetInt("Highscore");
                previousRunText.GetComponentInChildren<Text>().text = "Last Run: " + PlayerPrefs.GetInt("PreviousRun");
            } else {
				PlayerPrefs.SetInt ("Highscore", 0);
				PlayerPrefs.SetInt ("PreviousRun", 0);
                highscoreText.GetComponentInChildren<Text>().text = "Highscore: 0";
                previousRunText.GetComponentInChildren<Text>().text = "Highscore: 0";
			}

            instance = this;
		}else if(instance != this)
			Destroy (gameObject);
	}

	void Update()
	{
		
		if(startText.enabled == true)
			startText.color = new Color(startText.color.r, startText.color.g, startText.color.b, (Mathf.Sin(Time.time * 2.0f) + 1.0f)/2.0f);
		
		if (Input.GetKeyDown(KeyCode.Escape)) { Application.Quit(); }
        if (startGame && Input.GetMouseButtonDown(0)){
            startGame = false;
            startText.enabled = false;
			highscoreText.SetActive(false);
			previousRunText.SetActive(false);
			scoreText.enabled = true;
			scrollingObjects = GetComponentsInChildren<ScrollingObject> ();
            jumpReady = true;
            //if (autoLoad)
            //    maps[currentMap] = (GameObject)Instantiate(superEasyMaps[rand], objectPoolPosition, Quaternion.identity);
            for (int i = 0; i < scrollingObjects.Length; i++) {
				scrollingObjects [i].enabled = true;
			}


			InvokeRepeating("scoreCount", 1.5f, 1.5f);
			return;
        } else if (startGame) {
            return;
        }
        //If the game is over and the player has pressed some input...
        if (gameOver && Input.GetMouseButtonUp(0)) {
			//...reload the current scene.
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}

		if (Input.GetMouseButtonDown (0) && jumpReady) {
			players = gameObject.GetComponentsInChildren<PlayerMovement>();
			foreach (PlayerMovement player in players) {
				player.Jump ();
			}
		}

//		score++;
//		scoreText.text = "Score: " + score.ToString();
	}

    //public void LoadMap()
    //{
    //    if (currentMap == 0) {
    //        currentMap = 1;					
    //    }  else {
    //        currentMap = 0;
    //    }
    //    if (loadedMaps < 5) {
    //        rand = Random.Range(0, easyI);
    //        maps[currentMap] = (GameObject)Instantiate(easyMaps[rand], objectPoolPosition, Quaternion.identity);
    //    }
    //    else if(loadedMaps < 10) {
    //        rand = Random.Range(0, mediumI);
    //        maps[currentMap] = (GameObject)Instantiate(mediumMaps[rand], objectPoolPosition, Quaternion.identity);
    //    } else {
    //        rand = Random.Range(0, hardI);
    //        maps[currentMap] = (GameObject)Instantiate(hardMaps[rand], objectPoolPosition, Quaternion.identity);
    //    }
    //    loadedMaps++;
    //}


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
		if (score > PlayerPrefs.GetInt("Highscore")){
			PlayerPrefs.SetInt ("Highscore", score);
		}
		PlayerPrefs.SetInt ("PreviousRun", score);

		jumpReady = false;
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