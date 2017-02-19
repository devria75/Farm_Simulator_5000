using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class growPlants : MonoBehaviour {

	public Material[] materials;
	public Renderer rend;
	int toGrow = 3;
	float timeLeft;
	int timer = 30;
	int cost = 10;

	// Use this for initialization
	void Start () {
		rend.sharedMaterial = materials [materials.Length-1];
	}
	
	// Update is called once per frame
	void Update () {
		timeLeft -= Time.deltaTime;
	}

	void OnMouseDown(){
		if (timeLeft < 0 && toGrow >= 0) {
			rend.sharedMaterial = materials [toGrow];
			toGrow -= 1;
			timeLeft = timer;
		} 
		else if (toGrow == 0) {
		//sell the plant
			FindObjectOfType<playerResources> ().addMoney (cost);
		//then I guess just destroy game object
		}
	}
}
