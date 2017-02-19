using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuySeeds : MonoBehaviour {

	int cost = 5;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown(){
		if (playerResources.money >= 0) {
			FindObjectOfType<playerResources> ().subtractMoney (cost);
		}
	}
}
