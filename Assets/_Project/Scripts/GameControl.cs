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

	private bool _jumpReady = false;
    public bool startGame = true;

	PlayerMovement[] players;


//    public int mapPoolSize = 3;
    private GameObject[] easyMaps;
    private GameObject[] mediumMaps;
    private GameObject[] hardMaps;
    private GameObject firstObject;
    private int easyI;
    private int mediumI;
    private int hardI;
    private int rand;
    //	public float spawnRate = 4f;

    private GameObject[] maps;
    private Vector2 objectPoolPosition = new Vector2(5f, 0);
    //	private float timeSinceLastSpawned;
    //	private float spawnXPosition = 10f;
    private int currentMap = 0;
    private int loadedMaps = 1;

	[Header("Disable this to test maps")]
	public bool autoLoad = true;
    

    void Awake()
	{
		if (instance == null){
			audio = GetComponent<AudioSource> ();
            maps = new GameObject[2];
            easyMaps = Resources.LoadAll<GameObject>("PrefabMaps/Easy");
            easyI = easyMaps.GetLength(0);
            mediumMaps = Resources.LoadAll<GameObject>("PrefabMaps/Medium");
            mediumI = mediumMaps.GetLength(0);
            hardMaps = Resources.LoadAll<GameObject>("PrefabMaps/Hard");
            hardI = hardMaps.GetLength(0);
            rand = Random.Range(0, easyI);
            //		Debug.Log(rand);
			if (autoLoad)
            	maps[currentMap] = (GameObject)Instantiate(easyMaps[rand], objectPoolPosition, Quaternion.identity);
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

    public void LoadMap()
    {
        if (currentMap == 0) {
            currentMap = 1;
        }  else {
            currentMap = 0;
        }
        if (loadedMaps < 5) {
            rand = Random.Range(0, easyI);
            maps[currentMap] = (GameObject)Instantiate(easyMaps[rand], objectPoolPosition, Quaternion.identity);
        }
        else if(loadedMaps < 10) {
            rand = Random.Range(0, mediumI);
            maps[currentMap] = (GameObject)Instantiate(mediumMaps[rand], objectPoolPosition, Quaternion.identity);
        } else {
            rand = Random.Range(0, hardI);
            maps[currentMap] = (GameObject)Instantiate(hardMaps[rand], objectPoolPosition, Quaternion.identity);
        }
        loadedMaps++;
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