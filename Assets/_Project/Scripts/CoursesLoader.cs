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
	private List<GameObject> currentMaps = new List<GameObject>();
    private GameObject firstObject;
    private Vector2 objectPoolPosition = new Vector2(4f, 0);
    private int superEasyI;
    private int easyI;
    private int mediumI;
    private int hardI;
    private int rand;
    private int currentMap = 0;
    private int loadedMaps = 0;
	private int difficulty = 1;

	private int mapCourse = 1;
	private int bgToCourse = 1;
	private string mapTag = "Grass";

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
			foreach(GameObject map in easyMaps){
				if (map.tag.Contains("Grass")) {
					currentMaps.Add(map);
				}
			}
        }  else if (instance != this)
            Destroy(gameObject);
    }

	public void FirstCourse(){
		maps[currentMap] = (GameObject)Instantiate(superEasyMaps[rand], objectPoolPosition, Quaternion.identity);
	}

	public void MapsToUse(){
		currentMaps.Clear ();
		if (bgToCourse == 1) {
			mapTag = "Grass";
		} else if (bgToCourse == 2) {
			mapTag = "City";
		} else if (bgToCourse == 3) {
			mapTag = "Lava";
		}
		if (difficulty == 1) {
			foreach (GameObject map in easyMaps) {
				if (map.tag.Contains (mapTag)) {
					currentMaps.Add (map);
				}
			}
		} else if (difficulty == 2) {
			foreach (GameObject map in mediumMaps) {
				if (map.tag.Contains (mapTag)) {
					currentMaps.Add (map);
				}
			}
		} else {
			foreach (GameObject map in hardMaps) {
				if (map.tag.Contains (mapTag)) {
					currentMaps.Add (map);
				}
			}
		}
	}

    public void LoadMap()
    {
		mapCourse++;
//		testing += 9;
//		Debug.Log ("Course:" + testing);

		if (mapCourse == 4) {
			mapCourse = 0;
			bgToCourse = GameControl.instance.goingToBg;
			// Execute MapsToUse()
			MapsToUse ();
		}

		if (loadedMaps % 5 == 0 && loadedMaps != 0) {
			Debug.Log (loadedMaps);
			difficulty++;
			MapsToUse ();
		}

        if (currentMap == 0) {
            currentMap = 1;
        } else {
            currentMap = 0;
        }

		rand = Random.Range(0, currentMaps.Count);
		maps[currentMap] = (GameObject)Instantiate(currentMaps[rand], objectPoolPosition, Quaternion.identity);

//        if (loadedMaps < 5) {
//			// Execute MapsToUse()
//            rand = Random.Range(0, easyI);
//            maps[currentMap] = (GameObject)Instantiate(easyMaps[rand], objectPoolPosition, Quaternion.identity);
//        } else if (loadedMaps < 10) {
//			// Execute MapsToUse()
//            rand = Random.Range(0, mediumI);
//            maps[currentMap] = (GameObject)Instantiate(mediumMaps[rand], objectPoolPosition, Quaternion.identity);
//        } else {
////			 Execute MapsToUse()
//            rand = Random.Range(0, hardI);
//            maps[currentMap] = (GameObject)Instantiate(hardMaps[rand], objectPoolPosition, Quaternion.identity);
//        }
        loadedMaps++;
    }
}
