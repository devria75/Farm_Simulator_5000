using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class growPlants : MonoBehaviour {
	//timer info
	public float timeLeft;
	public int timer = 120;
	public int waterConsumeTime = 60;
	public int deathTime = 180;
	public bool isWatered = false;

	//player starts not in collider
	public bool playerInCol = false;

	//game objects being instantiated or destroyed
	private GameObject plant;
	public GameObject deadSprout;
	public GameObject Carrot;
	public GameObject Corn;
	public GameObject Potato;
	public GameObject Sprout;
	public GameObject Tomato;

	//variables to keep track of what's in the plot
	public bool plotFilled;
	public PlotStatus plantInPlot;

	//audio stuff
	public AudioClip water;
	public AudioSource audio;
	public AudioClip sell_Noise;
	public AudioClip dirt;

	//prices for all components when sold
	public enum PlotStatus {
		Empty=0, Corn=6, Carrot=8, Potato=8, Tomato=10
	};

	//text placement
	public int dirtVerticalOffset = 100;
	public Text dirtText;
	public int sellVerticalOffset = 100;
	public Text sellText;
	public int removeVerticalOffset = 100;
	public Text removeText;
	public int waterVerticalOffset = 50;
	public Text waterText;

	//character resources
	public playerResources characterResources;

	//watering
	public GameObject waterPrefab;

	//color materials for sprouts
	//public Material[] plantColor;
	//public Renderer rend;
	//int assignedPlantColor = 5;
	//public Material[] dirtColor;
	//int assignedDirtColor = 5;

	// Use this for initialization
	void Start () {
		//start with no plots filled
		plotFilled = false;

		//get playerResources script on start
		GameObject playerObj = GameObject.FindGameObjectWithTag ("Player");
		characterResources = playerObj.GetComponent<playerResources> ();

		//LOOK AT ME IF AUDIO IS NOT PLAYING THE RIGHT SOUND
		audio = GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update () {
		timeLeft -= Time.deltaTime;

		//if player is in collider then check to see what input they press: 1,2,3,4
		if (playerInCol == true) {

			//CORN
			if (Input.GetKeyDown("1") && plotFilled == false && characterResources.cornCounter > 0){
				audio.PlayOneShot (dirt);
				plant = Instantiate(Sprout, transform.position+Vector3.up*.2f, Quaternion.identity) as GameObject;
				timeLeft = timer;
				plantInPlot = PlotStatus.Corn;
				plotFilled = true;
				characterResources.cornCounter--;
			}

			//CARROT
			if (Input.GetKeyDown("2") && plotFilled == false && characterResources.carrotCounter > 0){
				audio.PlayOneShot (dirt);
				plant = Instantiate(Sprout, transform.position+Vector3.up*.2f, Quaternion.identity) as GameObject;
				timeLeft = timer;
				plantInPlot = PlotStatus.Carrot;
				plotFilled = true;
				characterResources.carrotCounter--;
			}

			//TOMATO
			if (Input.GetKeyDown("3") && plotFilled == false && characterResources.tomatoCounter > 0){
				audio.PlayOneShot (dirt);
				plant = Instantiate(Sprout, transform.position+Vector3.up*.2f, Quaternion.identity) as GameObject;
				timeLeft = timer;
				plantInPlot = PlotStatus.Tomato;
				plotFilled = true;
				characterResources.tomatoCounter--;
			}

			//POTATO
			if (Input.GetKeyDown("4") && plotFilled == false && characterResources.potatoCounter > 0){
				audio.PlayOneShot (dirt);
				plant = Instantiate(Sprout, transform.position+Vector3.up*.2f, Quaternion.identity) as GameObject;
				timeLeft = timer;
				plantInPlot = PlotStatus.Potato;
				plotFilled = true;
				characterResources.potatoCounter--;
			}
		}

		//if plot is filled and shift key is pressed, water plant
		if (Input.GetKeyDown(KeyCode.LeftShift) && plotFilled == true) {
			//play audio
			Debug.Log("Sound.");
			audio.PlayOneShot (water);
			//emit water particles
			GameObject waterObject = Instantiate (waterPrefab, transform.position, Quaternion.identity) as GameObject;
			isWatered = true;
		}

		//plant dies if time runs out with no water
		if (plotFilled == true && timeLeft <= 0 && isWatered == false) {
			Destroy (plant);
			plant = Instantiate(deadSprout, transform.position+Vector3.up*.2f, Quaternion.identity) as GameObject;
		}

		//if plant is done growing, turn into finished crop
		if (plotFilled == true && timeLeft <= 0 && isWatered == true) {
			//CORN
			if (plantInPlot == PlotStatus.Corn){
				Destroy (plant);
				plant = Instantiate(Corn, transform.position+Vector3.up*.3f, Quaternion.identity) as GameObject;
			}

			//CARROT
			if (plantInPlot == PlotStatus.Carrot){
				Destroy (plant);
				plant = Instantiate(Carrot, transform.position+Vector3.up*.3f, Quaternion.identity) as GameObject;
			}

			//TOMATO
			if (plantInPlot == PlotStatus.Tomato){
				Destroy (plant);
				plant = Instantiate(Tomato, transform.position+Vector3.up*.3f, Quaternion.identity) as GameObject;
			}

			//POTATO
			if (plantInPlot == PlotStatus.Potato){
				Destroy (plant);
				plant = Instantiate(Potato, transform.position+Vector3.up*.3f, Quaternion.identity) as GameObject;
			}
		}

		//If plot is filled, and time has run out, and player has watered it, show sell text
		if (plotFilled == true && timeLeft <= 0 && playerInCol && isWatered == true) {
			Vector3 newPos = sellText.rectTransform.position;
			newPos.y = sellVerticalOffset;
			sellText.rectTransform.position = newPos;

			//if player presses F then add the money that each plant is worth (see array at top for amounts)
			if (Input.GetKeyDown ("f")) {
				audio.PlayOneShot (sell_Noise);
				FindObjectOfType<playerResources> ().addMoney ((int)plantInPlot);
				plantInPlot = PlotStatus.Empty;
				plotFilled = false;
				Destroy (plant);
				
			}
		}

		//If plot is filled and time has run out show remove text
		if (plotFilled == true && timeLeft <= 0 && playerInCol && isWatered == false) {
			Vector3 newPos = removeText.rectTransform.position;
			newPos.y = removeVerticalOffset;
			removeText.rectTransform.position = newPos;

			//if player presses F then add the money that each plant is worth (see array at top for amounts)
			if (Input.GetKeyDown ("f")) {
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
		if (plotFilled == true) {
			Vector3 newPos2 = waterText.rectTransform.position;
			newPos2.y = waterVerticalOffset;
			waterText.rectTransform.position = newPos2;
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

		Vector3 newPos3 = removeText.rectTransform.position;
		newPos3.y = removeVerticalOffset * -1;
		removeText.rectTransform.position = newPos3;

		Vector3 newPos4 = waterText.rectTransform.position;
		newPos4.y = waterVerticalOffset * -1;
		waterText.rectTransform.position = newPos4;

		playerInCol = false;
	}
		
}