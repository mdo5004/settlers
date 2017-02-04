using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlaySystem : MonoBehaviour
{
	
	public static int numPlayers = 3;
	private static int whoseTurn;

	public static int turnNumber;
	public static int WhoseTurn {
		get {
			return whoseTurn;
		}
	}

	public bool pieceIsBeingPlaced;

	public GameObject[] playerPrefabs;
	private GameObject[] players;

	public static Dictionary<string, Card> resources;
	public static Dictionary<string, Card> developmentCards;
	public static Dictionary<string, Card> specialCards;

	public GameObject userInterface;
	Button[] buttons;
	PlayerScript playerScript;
	public Text whoseTurnIsIt;

	public Image[] dice;
	private bool diceHaveBeenRolled;
	public Sprite[] dieFaces;

	// Use this for initialization
	void Awake ()
	{
		diceHaveBeenRolled = true;
		DeckSetup ();
		PlayerSetup ();

		turnNumber = -1;
		pieceIsBeingPlaced = false;
		whoseTurn = -1; // starts at -1 so NextTurn starts at 0.

		buttons = userInterface.GetComponentsInChildren<Button> ();

	}

	public void NextTurn ()
	{
		whoseTurn++;
		diceHaveBeenRolled = false;

		if (whoseTurn >= numPlayers) {
			whoseTurn = 0;
			turnNumber++;
		}

		Debug.Log ("Next turn: " + players [whoseTurn].name);
		whoseTurnIsIt.text = players [whoseTurn].name;

		playerScript = players [whoseTurn].GetComponent<PlayerScript> ();
		if (turnNumber > 0){
			Debug.Log ("Turn number = " + turnNumber);
			playerScript.WhatCanIAfford ();
		}


		UpdateUI ();


	}

	public void RollDice() {
		if(!diceHaveBeenRolled) {
			int diceTotal = 2;
			foreach (Image die in dice){
				int rolled = Random.Range (0, 6);
				diceTotal += rolled;
				die.sprite = dieFaces [rolled];
			}
			diceHaveBeenRolled = true;
			Debug.Log ("Roll is " + diceTotal);
		}
	}

	void UpdateUI(){
		foreach (Button button in buttons) {
			if (button.gameObject.name == "Build Road") {
				if (playerScript.canBuildRoad) {
					button.interactable = true;
				} else {
					button.interactable = false;
				}
			}
			if (button.gameObject.name == "Build Settlement") {
				if (playerScript.canBuildSettlement) {
					button.interactable = true;
				} else {
					button.interactable = false;
				}
			}
			if (button.gameObject.name == "Build City") {
				if (playerScript.canBuildCity) {
					button.interactable = true;
				} else {
					button.interactable = false;
				}
			}
			if (button.gameObject.name == "Build Dev Card") {
				if (playerScript.canBuyDevelopmentCard) {
					button.interactable = true;
				} else {
					button.interactable = false;
				}
			}
		}
	}


	GameObject pieceBeingPlaced;

	public void BuildRoad() {
		Debug.Log("Trying to build a road");
		PlayerScript playerScript = players [whoseTurn].GetComponent<PlayerScript> ();
		if(playerScript.canBuildRoad && !pieceIsBeingPlaced) {
			pieceIsBeingPlaced = true;
			pieceBeingPlaced = (GameObject)Instantiate (playerScript.gamePieces [0],Vector3.zero,Quaternion.identity);
		}
	}
	public void BuildSettlement() {
		if(playerScript.canBuildSettlement && !pieceIsBeingPlaced) {
			pieceIsBeingPlaced = true;
			pieceBeingPlaced = (GameObject)Instantiate (playerScript.gamePieces [1],Vector3.zero,Quaternion.identity);
		}
	}
	public void BuildCity() {
		if(playerScript.canBuildCity && !pieceIsBeingPlaced) {
			pieceIsBeingPlaced = true;
			pieceBeingPlaced = (GameObject)Instantiate (playerScript.gamePieces [2],Vector3.zero,Quaternion.identity);
		}
	}
	public void BuyDevCard() {

	}


	// Update is called once per frame
	void Update ()
	{

		if (pieceIsBeingPlaced) {
			RaycastHit[] mouseHit = new RaycastHit[1];
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition); 
			mouseHit = Physics.RaycastAll (ray);
			if (mouseHit.Length > 0){

				pieceBeingPlaced.transform.position = mouseHit [0].point;

			}


			if (Input.GetMouseButtonDown (0)) {
			

				foreach (RaycastHit hit in mouseHit) {

					if (hit.collider.CompareTag ("Facet")) {

					} else if (hit.collider.CompareTag ("Edge")) {

					}


				}
			}
		}
	}


	void DeckSetup () {

		resources = new Dictionary<string, Card> ();

		resources.Add ("Ore", new Card ("Ore", "One ore resouce", 19, 0));
		resources.Add ("Brick", new Card ("Brick", "One brick resouce", 19, 0));
		resources.Add ("Grain", new Card ("Grain", "One grain resouce", 19, 0));
		resources.Add ("Wool", new Card ("Wool", "One wool resouce", 19, 0));
		resources.Add ("Lumber", new Card ("Lumber", "One lumber resouce", 19, 0));

		developmentCards = new Dictionary<string, Card> ();

		developmentCards.Add ("Soldier", new Card ("Soldier", "Move the robber. Steal one resource card from the owner of an adjacent settlement or city.",14,0));
		developmentCards.Add ("Year of Plenty", new Card ("Year of Plenty", "take any 2 resource cards from the bank and add them to your hand. They can be two different resources of two of the same resource. They may immediately be used to build.",3,0));
		developmentCards.Add ("Road Building", new Card ("Road Building", "Place 2 new roads as if you had just built them.",3,0));
		developmentCards.Add ("Monopoly", new Card ("Monopoly", "When you play this card, announce one type of resource. All other players must give you all their resource cards of that type.",3,0));
		developmentCards.Add ("Victory Point", new Card ("Victory Point", "1 victory point!", 5, 1));

		specialCards = new Dictionary<string, Card> ();

		specialCards.Add ("Longest Road", new Card ("Longest Road", "2 victory points! This card goes to the player with the longest road of at least five road segments. Another player who builds a longer road gets this card.", 1, 2));
		specialCards.Add ("Largest Army", new Card ("Largest Army", "2 victory points! This card goes to the player with the largest army of at least three soldiers. Another player who creates a larger army gets this card.", 1, 2));

	}
	public static int DevelopmentCardsLeft ()
	{
		int dCardsLeft = 0;
		foreach(string key in developmentCards.Keys){
			dCardsLeft += developmentCards [key].quantity;
		}
		return dCardsLeft;
	}

	void PlayerSetup ()
	{
		players = new GameObject[numPlayers];
		for (int i = 0; i < numPlayers; i++) {
			GameObject tempPlayer = (GameObject)Instantiate (playerPrefabs[i]);
			PlayerScript playerScript = tempPlayer.GetComponent<PlayerScript> ();
			playerScript.playerNumber = i;

			players [i] = tempPlayer;

		}
	}

}
