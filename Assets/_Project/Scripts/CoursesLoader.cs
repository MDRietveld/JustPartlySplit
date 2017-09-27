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
    private GameObject firstObject;
    private Vector2 objectPoolPosition = new Vector2(7f, 0);
    private int superEasyI;
    private int easyI;
    private int mediumI;
    private int hardI;
    private int rand;
    private int currentMap = 0;
    private int loadedMaps = 0;

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

    public void LoadMap()
    {
        if (currentMap == 0) {
            currentMap = 1;
        } else {
            currentMap = 0;
        }
        if (loadedMaps < 5) {
            rand = Random.Range(0, easyI);
            maps[currentMap] = (GameObject)Instantiate(easyMaps[rand], objectPoolPosition, Quaternion.identity);
        } else if (loadedMaps < 10) {
            rand = Random.Range(0, mediumI);
            maps[currentMap] = (GameObject)Instantiate(mediumMaps[rand], objectPoolPosition, Quaternion.identity);
        } else {
            rand = Random.Range(0, hardI);
            maps[currentMap] = (GameObject)Instantiate(hardMaps[rand], objectPoolPosition, Quaternion.identity);
        }
        loadedMaps++;
    }
}
