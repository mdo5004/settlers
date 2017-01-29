using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaterGameBoard : MonoBehaviour {

	public Object[] tilePrefabs;
	[HideInInspector]
	public Transform[] tileLocations; 
	public Dictionary<string, GameObject> gameTiles; // This will hold all the game tiles

	// Use this for initialization
	void Awake () {

		tileLocations = GetComponentsInChildren<Transform> ();
		gameTiles = new Dictionary<string, GameObject> ();

	}

	void Start ()
	{
		foreach (Transform location in tileLocations) {
			if (location.gameObject.name != "Water Tiles") {
				GameObject tempTile = (GameObject)Instantiate (tilePrefabs [0], location.position, Quaternion.identity);
				Debug.Log ("Name: " + location.gameObject.name);
				Debug.Log ("Temp tile: " + tempTile.ToString());


				gameTiles.Add (location.gameObject.name, tempTile);
			}
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
