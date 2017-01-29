using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlaySystem : MonoBehaviour
{
	
	public static int numPlayers;
	private static int whoseTurn;

	public static int WhoseTurn {
		get {
			return whoseTurn;
		}
	}

	public static bool pieceIsBeingPlaced;

	public GameObject playerPrefab;
	private GameObject[] playerArray;

	public static Dictionary<string, Card> resources;
	public static Dictionary<string, Card> developmentCards;
	public static Dictionary<string, Card> specialCards;



	// Use this for initialization
	void Start ()
	{
		DeckSetup ();
		PlayerSetup ();

		pieceIsBeingPlaced = false;
		whoseTurn = 0;


	}

	public static void NextTurn ()
	{
		whoseTurn++;

		if (whoseTurn >= numPlayers) {
			whoseTurn = 0;
		}
	}
	// Update is called once per frame
	void Update ()
	{

		if (pieceIsBeingPlaced) {
			RaycastHit[] mouseHit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition); 
			mouseHit = Physics.RaycastAll (ray);



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
		playerArray = new GameObject[numPlayers];
		for (int i = 0; i < numPlayers; i++) {
			GameObject tempPlayer = (GameObject)Instantiate (playerPrefab);
			PlayerScript playerScript = tempPlayer.GetComponent<PlayerScript> ();
			playerScript.playerNumber = i;

			playerArray [i] = tempPlayer;

		}
	}

}
