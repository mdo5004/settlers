using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameBoard : MonoBehaviour {

	public Dictionary<string, GameObject> gameTiles; // This will hold all the game tiles

	public Dictionary<string, int> pieceCount; // This holds the number of tiles of each kind
	public Transform[] tileLocations; 
	public GameObject[] tilePrefabs;

	public int[] gameTileNumbers = {2, 5, 4, 6, 3, 9, 8, 11, 11, 10, 6, 3, 8, 4, 8, 10, 11, 12, 10, 4, 9, 5, 5, 9, 12, 3, 2, 6};
	private int gtn = 0;

	void Awake () {

		pieceCount = new Dictionary<string, int> ();

		pieceCount.Add ("Ore Tile", 3); // 3
		pieceCount.Add ("Brick Tile", 3); // 6
		pieceCount.Add ("Wheat Tile", 4); // 10
		pieceCount.Add ("Wood Tile", 4); // 14
		pieceCount.Add ("Sheep Tile", 4); // 18
		pieceCount.Add ("Desert Tile", 1); // 19

		tileLocations = GetComponentsInChildren<Transform> ();

		gameTiles = new Dictionary<string, GameObject> ();

	}
	// Use this for initialization
	void Start ()
	{

		SetupGameTiles ();

	}

	void SetupGameTiles ()
	{
		foreach (Transform location in tileLocations) {
			if (location.gameObject.name != "Game Board") {
				int random;
				int choice;
				int n = 0;

				do {

					random = Random.Range (0, 19);

					if (random < 3) {
						choice = 0;
					} else if (random >= 3 && random < 6) {
						choice = 1;
					} else if (random >= 6 && random < 10) {
						choice = 2;
					} else if (random >= 10 && random < 14) {
						choice = 3;
					} else if (random >= 14 && random < 18) {
						choice = 4;
					} else {
						choice = 5;
					} 

					n++;
				} while (pieceCount [tilePrefabs [choice].name] <= 0 && n < 50);


				pieceCount [tilePrefabs [choice].name] = pieceCount [tilePrefabs [choice].name] - 1;
				GameObject tempPiece = (GameObject)Instantiate (tilePrefabs [choice], location.position, Quaternion.identity);
				if (tempPiece.tag == "Numbered") {
					GameTile tempPieceComponent = tempPiece.GetComponent<GameTile> ();
					tempPieceComponent.MyNumber = gameTileNumbers [gtn];
					gtn++;
				}
				gameTiles.Add (location.gameObject.name, tempPiece);

			}
		}
	}
	

}
