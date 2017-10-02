using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoursesLoader : MonoBehaviour {
    public static CoursesLoader instance;
    private GameObject[] superEasyMaps;
    private GameObject[] easyMaps;
    private GameObject[] mediumMaps;
    private GameObject[] hardMaps;
    private GameObject[] maps;
	private GameObject[] currentMaps;
    private GameObject firstObject;
    private Vector2 objectPoolPosition = new Vector2(4f, 0);
    private int superEasyI;
    private int easyI;
    private int mediumI;
    private int hardI;
    private int rand;
    private int currentMap = 0;
    private int loadedMaps = 0;
	private int testing = 0;

	private int mapCourse = 1;
	private int bgToCourse = 1;

//    private bool startLoading = true;
    // Use this for initialization
    void Awake () {
        if (instance == null) {
            maps = new GameObject[2];
            superEasyMaps = Resources.LoadAll<GameObject>("PrefabMaps/SuperEasy");
            superEasyI = superEasyMaps.GetLength(0);
            easyMaps = Resources.LoadAll<GameObject>("PrefabMaps/Easy");
            easyI = easyMaps.GetLength(0);
            mediumMaps = Resources.LoadAll<GameObject>("PrefabMaps/Medium");
            mediumI = mediumMaps.GetLength(0);
            hardMaps = Resources.LoadAll<GameObject>("PrefabMaps/Hard");
            hardI = hardMaps.GetLength(0);
            rand = Random.Range(0, superEasyI);
            instance = this;
        }  else if (instance != this)
            Destroy(gameObject);
    }

	public void FirstCourse(){
		maps[currentMap] = (GameObject)Instantiate(superEasyMaps[rand], objectPoolPosition, Quaternion.identity);
	}

	public void MapsToUse(){
		// use bgToCourse to load in the current maps

		// Foreach the current difficulty map array
		// Filter on TAG

		// Fill the filtered tag in a new GameObject array (currentMaps)
	}

    public void LoadMap()
    {
		mapCourse++;
		testing += 9;
		Debug.Log ("Course:" + testing);

		if (mapCourse == 4) {
			mapCourse = 0;
			bgToCourse = GameControl.instance.goingToBg;
			// Execute MapsToUse()
		}

        if (currentMap == 0) {
            currentMap = 1;
        } else {
            currentMap = 0;
        }
        if (loadedMaps < 5) {
			// Execute MapsToUse()
            rand = Random.Range(0, easyI);
            maps[currentMap] = (GameObject)Instantiate(easyMaps[rand], objectPoolPosition, Quaternion.identity);
        } else if (loadedMaps < 10) {
			// Execute MapsToUse()
            rand = Random.Range(0, mediumI);
            maps[currentMap] = (GameObject)Instantiate(mediumMaps[rand], objectPoolPosition, Quaternion.identity);
        } else {
			// Execute MapsToUse()
            rand = Random.Range(0, hardI);
            maps[currentMap] = (GameObject)Instantiate(hardMaps[rand], objectPoolPosition, Quaternion.identity);
        }
        loadedMaps++;
    }
}
