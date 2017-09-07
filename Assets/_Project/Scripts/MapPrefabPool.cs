using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPrefabPool : MonoBehaviour {

	public int mapPoolSize = 3;
	private GameObject[] easyMaps;
	private GameObject[] mediumMaps;
	private GameObject[] hardMaps;
	private int mapI;
	private int rand;
//	public float spawnRate = 4f;

	private GameObject[] maps;
	private Vector2 objectPoolPosition = new Vector2 (5f, 0);
//	private float timeSinceLastSpawned;
//	private float spawnXPosition = 10f;
	private int currentMap = 0;

	// Use this for initialization
	void Start () {
		maps = new GameObject[mapPoolSize];
		easyMaps = Resources.LoadAll<GameObject> ("PrefabMaps/Easy");
		mediumMaps = Resources.LoadAll<GameObject> ("PrefabMaps/Medium");
		hardMaps = Resources.LoadAll<GameObject> ("PrefabMaps/Hard");
		mapI = easyMaps.GetLength(0);
		rand = Random.Range(0, mapI);
//		Debug.Log(rand);
		maps[currentMap] = (GameObject)Instantiate (easyMaps[rand], objectPoolPosition, Quaternion.identity);
//		RectTransform test = (RectTransform)easyMaps[rand].transform;
//		Debug.Log (test.rect.width);
		calcBound(maps[currentMap]);

	}
	public static void calcBound(GameObject root)
	{
		foreach (Transform child in root.transform)
		{
			if (!child.gameObject.GetComponent<BoxCollider2D>())
				child.gameObject.AddComponent<BoxCollider2D>();

		}

		BoxCollider2D[] boxCollider = root.GetComponentsInChildren<BoxCollider2D>();

		Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);

		foreach (BoxCollider2D bc in boxCollider)
		{
			bounds.Encapsulate(bc.bounds);
		}

		Debug.Log("BoxCollider bounds: " + bounds.ToString());
	}

	// Update is called once per frame
	void Update () {
		if (GameControl.instance.gameOver == false) {
			
		}
//		timeSinceLastSpawned += Time.deltaTime;
//
//		if (GameControl.instance.gameOver == false && timeSinceLastSpawned >= spawnRate) {
//			timeSinceLastSpawned = 0;
//
//			columns [currentColumn].transform.position = new Vector2 (spawnXPosition, 0);
//			currentColumn++;
//			if (currentColumn >= columnPoolSize) {
//				currentColumn = 0;
//			}
//		}
	}
}
