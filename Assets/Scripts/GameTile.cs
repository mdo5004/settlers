using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameTile : MonoBehaviour {

	private int myNumber;

	public int MyNumber {
		get {
				return myNumber;
		}
		set {
				myNumber = value;
				number.text = myNumber.ToString();
				if (myNumber == 8 || myNumber == 6) {
					number.color = Color.red;
				}
		}
	}
	private Text number;

	// Use this for initialization
	void Awake () {
		number = GetComponentInChildren<Text> ();

	}
	

}
