using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class growPlants : MonoBehaviour {

	//timer info
	public float timeLeft;
	public int timer = 30;

	//player starts not in collider
	public bool playerInCol = false;

	//game objects being instantiated or destroyed
	private GameObject plant;
	public GameObject Carrot;
	public GameObject Corn;
	public GameObject Potato;
	public GameObject Sprout;
	public GameObject Tomato;

	//variables to keep track of what's in the plot
	public bool plotFilled;
	public PlotStatus plantInPlot;

	//prices for all components when sold
	public enum PlotStatus {
		Empty=0, Corn=6, Carrot=8, Potato=8, Tomato=10
	};

	//text placement
	public int dirtVerticalOffset = 100;
	public Text dirtText;
	public int sellVerticalOffset = 100;
	public Text sellText;

	//character resources
	public playerResources characterResources;

	// Use this for initialization
	void Start () {
		//start with no plots filled
		plotFilled = false;

		//get playerResources script on start
		GameObject playerObj = GameObject.FindGameObjectWithTag ("Player");
		characterResources = playerObj.GetComponent<playerResources> ();
	}
	
	// Update is called once per frame
	void Update () {
		timeLeft -= Time.deltaTime;

		//if player is in collider then check to see what input they press: 1,2,3,4
		if (playerInCol == true) {

			//CORN
			if (Input.GetKeyDown("1") && plotFilled == false && characterResources.cornCounter > 0){
				plant = Instantiate(Corn, transform.position, Quaternion.identity) as GameObject;
				timeLeft = timer;
				plantInPlot = PlotStatus.Corn;
				plotFilled = true;
				characterResources.cornCounter--;
			}

			//CARROT
			if (Input.GetKeyDown("2") && plotFilled == false && characterResources.carrotCounter > 0){
				plant = Instantiate(Carrot, transform.position+Vector3.down*.3f, Quaternion.identity) as GameObject;
				timeLeft = timer;
				plantInPlot = PlotStatus.Carrot;
				plotFilled = true;
				characterResources.carrotCounter--;
			}

			//TOMATO
			if (Input.GetKeyDown("3") && plotFilled == false && characterResources.tomatoCounter > 0){
				plant = Instantiate(Sprout, transform.position+Vector3.up*.3f, Quaternion.identity) as GameObject;
				timeLeft = timer;
				plantInPlot = PlotStatus.Tomato;
				plotFilled = true;
				characterResources.tomatoCounter--;
			}

			//POTATO
			if (Input.GetKeyDown("4") && plotFilled == false && characterResources.potatoCounter > 0){
				plant = Instantiate(Potato, transform.position+Vector3.up*.1f, Quaternion.identity) as GameObject;
				timeLeft = timer;
				plantInPlot = PlotStatus.Potato;
				plotFilled = true;
				characterResources.potatoCounter--;
			}
		}

		//If plot is filled and time has run out show sell text
		if (plotFilled == true && timeLeft <= 0 && playerInCol) {
			Vector3 newPos = sellText.rectTransform.position;
			newPos.y = sellVerticalOffset;
			sellText.rectTransform.position = newPos;

			//if player presses F then add the money that each plant is worth (see array at top for amounts)
			if (Input.GetKeyDown ("f")) {
				FindObjectOfType<playerResources> ().addMoney ((int)plantInPlot);
				plantInPlot = PlotStatus.Empty;
				plotFilled = false;
				Destroy (plant);
			}
		}
	}


	//Show text

	//on entering trigger zone show dirt text
	void OnTriggerEnter(Collider Col){
		if (plotFilled == false) {
			Vector3 newPos = dirtText.rectTransform.position;
			newPos.y = dirtVerticalOffset;
			dirtText.rectTransform.position = newPos;
		}
		playerInCol = true;
	}

	//on exiting trigger zone hide dirt text and/or sell text
	void OnTriggerExit(Collider Col){
		Vector3 newPos = dirtText.rectTransform.position;
		newPos.y = dirtVerticalOffset * -1;
		dirtText.rectTransform.position = newPos;

		Vector3 newPos2 = sellText.rectTransform.position;
		newPos2.y = sellVerticalOffset * -1;
		sellText.rectTransform.position = newPos2;

		playerInCol = false;
	}
		
}