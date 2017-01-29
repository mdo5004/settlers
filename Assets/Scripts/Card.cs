using UnityEngine;
using System.Collections;

public class Card {

	public string title;
	public string description;
	public int quantity;
	public int victoryPoints;

	public Card(string title, string description, int quantity, int victoryPoints) {
		this.title = title;
		this.description = description;
		this.quantity = quantity;
		this.victoryPoints = victoryPoints;
	}
}
