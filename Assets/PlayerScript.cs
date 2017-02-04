using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour {

public int playerNumber;
public Dictionary<string, int> availablePieces;
public bool isMyTurn;
public GameObject[] gamePieces;

public int score;
public Dictionary<string, Card> resourceCards;
public Dictionary<string, Card> developmentCards;


public bool canBuildRoad = true;
public bool canBuildSettlement = true;
public bool canBuildCity;
public bool canBuyDevelopmentCard;

	// Use this for initialization
	void Start () {
		
		availablePieces = new Dictionary<string, int> ();
		availablePieces.Add ("Settlement", 4);
		availablePieces.Add ("City", 4);
		availablePieces.Add ("Road", 15);


		resourceCards = PlaySystem.resources;
		foreach (string card in resourceCards.Keys){
			resourceCards [card].quantity = 0;
		}
		developmentCards = PlaySystem.developmentCards;
		foreach (string card in developmentCards.Keys){
			developmentCards [card].quantity = 0;
		}
	}
	


	public void WhatCanIAfford() {
		if (availablePieces ["Road"] > 0 && resourceCards ["Brick"].quantity > 0 && resourceCards ["Lumber"].quantity > 0) {
				canBuildRoad = true;
			} else {
				canBuildRoad = false;
			}

			if (availablePieces ["City"] > 0 && resourceCards ["Grain"].quantity > 1 && resourceCards ["Ore"].quantity > 2) {
				canBuildCity = true;
			} else {
				canBuildCity = false;
			}

			if (availablePieces ["Settlement"] > 0 && resourceCards ["Brick"].quantity > 0 && resourceCards ["Lumber"].quantity > 0 && resourceCards ["Grain"].quantity > 0 && resourceCards ["Wool"].quantity > 0) {
				canBuildSettlement = true;
			} else {
				canBuildSettlement = false;
			}

			if(PlaySystem.DevelopmentCardsLeft() > 0 && resourceCards["Wool"].quantity > 0 && resourceCards["Ore"].quantity > 0 && resourceCards["Grain"].quantity > 0){
				canBuyDevelopmentCard = true;
			} else {
				canBuyDevelopmentCard = false;
			}
	}



}
